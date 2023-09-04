import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { IAuthenticateUser, IUser } from '../models/user';
import { UserService } from '../services/user.service';

@Injectable()
export class NetworkInterceptor implements HttpInterceptor {

  
  constructor(private _userService: UserService) {}
  
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
    const user: IAuthenticateUser = JSON.parse(localStorage.getItem("user")!);
    return next.handle(this.addTokenToRequest(user, request)).pipe(
      catchError((err: any) => {
        if(err instanceof HttpErrorResponse) {
          switch(err.status) {
            case 401:
              return this.handleUnauthorizedError(user, request, next);
          }
        }
        // console.log(err);
        return throwError(() => err);
      })
    );
  }

  handleUnauthorizedError(user:IAuthenticateUser, request: HttpRequest<unknown>, next: HttpHandler) {
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
