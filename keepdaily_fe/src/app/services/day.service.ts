import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { Day } from '../models/calendar';

@Injectable({
  providedIn: 'root'
})
export class DayService {


  constructor(private _http: HttpClient, private _env: EnvService) { }

  postDay(data: FormData) {
    return this._http.post<Day>(this._env.APIOption.DayEndpoint, data);
  }

  putDay(data: FormData) {
    return this._http.put(this._env.APIOption.DayEndpoint, data);
  }

  deleteDay(id: number) {
    return this._http.delete(`${this._env.APIOption.DayEndpoint}/${id}`);
  }
}
