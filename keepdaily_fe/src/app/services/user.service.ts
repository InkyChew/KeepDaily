import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { IUser } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  register(data: FormData) {
    return this._http.post<IUser>(`${this._env.APIOption.UserEndpoint}/Register`, data);
  }

  login(data: FormData) {
    return this._http.post<IUser>(`${this._env.APIOption.UserEndpoint}/Login`, data);
  }

  getUser(id: number) {
    return this._http.get<IUser>(`${this._env.APIOption.UserEndpoint}/${id}`);
  }

  updateUser(user: IUser) {
    return this._http.put(this._env.APIOption.UserEndpoint, user);
  }
}
