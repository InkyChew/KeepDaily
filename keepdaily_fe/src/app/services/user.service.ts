import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { IAuthenticateUser, IUser } from '../models/user';
import { Router } from '@angular/router';
import { BehaviorSubject, Subject } from 'rxjs';
const USER_KEY = "user";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  user$ = new BehaviorSubject<IAuthenticateUser | undefined>(this.getCurrentUser());

  constructor(private _http: HttpClient, private _env: EnvService, private _router: Router) {}

  setCurrentUser(user: IAuthenticateUser) {
    this.user$.next(user);
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  getCurrentUser() {
    const userstr = localStorage.getItem(USER_KEY);
    if(userstr) return JSON.parse(userstr);
    else this._router.navigateByUrl('/login');
  }
  
  register(data: FormData) {
    return this._http.post<IUser>(`${this._env.APIOption.UserEndpoint}/Register`, data);
  }

  login(data: FormData) {
    return this._http.post<IAuthenticateUser>(`${this._env.APIOption.UserEndpoint}/Login`, data);
  }

  getAllUser(query: IUserQuery) {
    let qp = new HttpParams();
    if(query.name) qp = qp.set('name', query.name);    
    if(query.email) qp = qp.set('email', query.email);    
    return this._http.get<IUser[]>(this._env.APIOption.UserEndpoint, {params: qp});
  }

  getUser(id: number) {
    return this._http.get<IUser>(`${this._env.APIOption.UserEndpoint}/${id}`);
  }

  updateUser(user: IUser) {
    return this._http.put<IUser>(this._env.APIOption.UserEndpoint, user);
  }

  updatePassword(id: number, pwd: string) {
    const data = new FormData();
    data.append('password', pwd);
    return this._http.patch(`${this._env.APIOption.UserEndpoint}/${id}`, data);
  }

  getPhoto(u: IUser, defImg?: string) {
    const defaultImg = defImg ?? 'https://placehold.co/200';
    if(u.imgName && u.imgType) {
      switch(u.imgType) {
        case "Google":
        case "Line":
          return u.imgName;
        default:
          return `${this._env.APIOption.UserEndpoint}/Img?name=${u.imgName}&type=${u.imgType}`;
      }
    } else return defaultImg;
  }

  updatePhoto(id: number, data: FormData) {
    return this._http.patch(`${this._env.APIOption.UserEndpoint}/${id}/Img`, data);
  }

  deletePhoto(id: number, name: string, type: string) {
    return this._http.delete(`${this._env.APIOption.UserEndpoint}/${id}/Img?name=${name}&type=${type}`);
  }

  refreshToken(user: IAuthenticateUser) {
    return this._http.post<IAuthenticateUser>(`${this._env.APIOption.UserEndpoint}/RefreshToken`, user);
  }

  logout() {
    this.user$.next(undefined);
    localStorage.removeItem("user");
    this._router.navigateByUrl('/login');
  }

  loginWithGoogle(body: any) {
    this._http.post<IAuthenticateUser>('https://localhost:5000/api/OAuthGoogle', body);
  }
}

export interface IUserQuery {
  name?: string,
  email?: string
}