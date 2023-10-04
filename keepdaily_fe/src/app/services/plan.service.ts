import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { Plan } from '../models/calendar';

@Injectable({
  providedIn: 'root'
})
export class PlanService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAllPlan(uid?: number) {
    let params = new HttpParams();
    if(uid) params = params.set('uid', uid);
    return this._http.get<Array<Plan>>(this._env.APIOption.PlanEndpoint, { params: params });
  }

  getPlan(id: number, year?: number, month?: number) {
    let params = new HttpParams();
    if(year) params = params.set('year', year);
    if(month) params = params.set('month', month);
    return this._http.get<Plan>(`${this._env.APIOption.PlanEndpoint}/${id}`, { params: params });
  }

  getPlanVideo(id: number, start: string, end: string) {
    // start, end: day.year-day.month-day.date
    return `${this._env.APIOption.PlanEndpoint}/${id}/Video?start=${start}&end=${end}`;
  }

  postPlan(plan: Plan) {
    return this._http.post<Plan>(this._env.APIOption.PlanEndpoint, plan);
  }

  putPlan(plan: Plan) {
    return this._http.put<Plan>(this._env.APIOption.PlanEndpoint, plan);
  }

  deletePlan(id: number) {
    return this._http.delete(`${this._env.APIOption.PlanEndpoint}/${id}`);
  }
}
