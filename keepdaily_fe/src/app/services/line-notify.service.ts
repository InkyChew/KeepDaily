import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';

@Injectable({
  providedIn: 'root'
})
export class LineNotifyService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAuth() {
    return this._http.get(this._env.APIOption.LineNotifyEndpoint, { responseType: 'text' });
  }
}
