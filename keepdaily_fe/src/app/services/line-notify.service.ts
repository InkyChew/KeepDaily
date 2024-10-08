import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';

@Injectable({
  providedIn: 'root'
})
export class LineNotifyService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAuth(email: string) {
    return this._http.get(`${this._env.APIOption.LineNotifyEndpoint}?email=${email}`, { responseType: 'text' });
  }
}
