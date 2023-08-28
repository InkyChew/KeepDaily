import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';

@Injectable({
  providedIn: 'root'
})
export class ConfirmEmailService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  sendConfirmEmail(uid: number) {
    return this._http.post(this._env.APIOption.ConfirmEmailEndpoint, uid);
  }

  sendChangePasswordConfirmEmail(email: string) {
    const data = new FormData();
    data.append('email', email);
    return this._http.post(`${this._env.APIOption.ConfirmEmailEndpoint}/ChangePassword`, data);
  }
}
