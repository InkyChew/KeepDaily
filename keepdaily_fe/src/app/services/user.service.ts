import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { IAuthenticateUser, IUser } from '../models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _http: HttpClient, private _env: EnvService,
    private _router: Router) { }

  register(data: FormData) {
    return this._http.post<IUser>(`${this._env.APIOption.UserEndpoint}/Register`, data);
  }

  login(data: FormData) {
    return this._http.post<IAuthenticateUser>(`${this._env.APIOption.UserEndpoint}/Login`, data);
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

  refreshToken(user: IAuthenticateUser) {
    return this._http.post<IAuthenticateUser>(`${this._env.APIOption.UserEndpoint}/RefreshToken`, user);
  }

  logout() {
    localStorage.removeItem("user");
    this._router.navigateByUrl('/login');
  }
}
