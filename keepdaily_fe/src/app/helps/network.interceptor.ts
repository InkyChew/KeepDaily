import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, finalize, switchMap, tap, throwError, timeout } from 'rxjs';
import { IAuthenticateUser, IUser } from '../models/user';
import { UserService } from '../services/user.service';
import { HelperService } from '../services/helper.service';

@Injectable()
export class NetworkInterceptor implements HttpInterceptor {

  
  constructor(private _userService: UserService,
    private _helperService: HelperService) {}
  
  private addTokenToRequest(user: IAuthenticateUser, request: HttpRequest<unknown>): HttpRequest<unknown> {
    if (user && user.accessToken){
      request = request.clone({
        withCredentials: true,
        setHeaders:{
          Authorization:`Bearer ${user.accessToken}`
        }
      });
    }
    return request;
  }
  
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this._helperService.startLoading();
    const user: IAuthenticateUser = JSON.parse(localStorage.getItem("user")!);
    return next.handle(this.addTokenToRequest(user, request)).pipe(
      timeout(60000),
      catchError((err: any) => {
        if(err instanceof HttpErrorResponse) {
          switch(err.status) {
            case 0:
              this._helperService.showMsg("無法與伺服器連線。");
              break;
            case 400:
              this._helperService.showMsg(err.error);
              break;
            case 401:
              if(user) return this.refreshToken(user, request, next);
              break;
            case 500:
              this._helperService.showMsg("伺服器錯誤，請稍後再試。");
              break;
          }
        }
        return throwError(() => err);
      }),
      finalize(() => this._helperService.stopLoading())
    );
  }

  refreshToken(user:IAuthenticateUser, request: HttpRequest<unknown>, next: HttpHandler) {
    return this._userService.refreshToken(user).pipe(
      switchMap(res => {
        user.accessToken = res.accessToken;
        localStorage.setItem("user", JSON.stringify(user));
        return next.handle(this.addTokenToRequest(user, request));
      }),
      catchError(err => {
        this._userService.logout();
        return throwError(() => err);
      }) 
    );
  }
}
