import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAuthenticateUser } from '../models/user';

@Injectable()
export class NetworkInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const user: IAuthenticateUser = JSON.parse(localStorage.getItem("user")!);
    if (user && user.accessToken){
      request = request.clone({
        setHeaders:{
          Authorization:`Bearer ${user.accessToken}`
        }
      });
    }
    return next.handle(request);
  }
}
