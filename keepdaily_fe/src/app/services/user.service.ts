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
    return this._http.post(`${this._env.APIOption.UserEndpoint}/Login`, data);
  }
}
