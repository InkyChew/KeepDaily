import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { Plan } from '../models/calendar';
import { Observable, Subscriber } from 'rxjs';

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

  loadVideo(id: number, start: string, end: string): Observable<string> {
    const videoChunks: Uint8Array[] = [];
    const chunkSize = 1024 * 1024; // 1MB chunks
    let startByte = 0;

    const getNextChunk = (observer: Subscriber<string>, name: string) => {
      const endByte = startByte + chunkSize - 1;
      const byteRange = `bytes=${startByte}-${endByte}`;
      console.log(byteRange);

      const headers = new HttpHeaders().set('Range', byteRange);
      const url = `${this._env.APIOption.PlanEndpoint}/Video?name=${name}`;

      this._http
        .get(url, {headers, responseType: 'arraybuffer', observe: 'response'})
        .subscribe((response: any) => {
            const chunk = new Uint8Array(response.body);
            videoChunks.push(chunk);

            startByte = endByte + 1;

            if (response.headers.has('Content-Range')) {
              // Continue fetching next chunk
              console.log(response.headers);
              
              getNextChunk(observer, name);
            } else {
              // All chunks fetched, concatenate and emit the videoUrl
              const blob = new Blob(videoChunks, { type: 'video/mp4' });
              const videoUrl = URL.createObjectURL(blob);
              observer.next(videoUrl);
              observer.complete();
            }
          });
    };

    
    return new Observable<string>((observer) => {
      const url = `${this._env.APIOption.PlanEndpoint}/${id}/Video?start=${start}&end=${end}`;

      this._http.get<string>(url, {responseType: 'text' as 'json'})
        .subscribe(name => {
          getNextChunk(observer, name);
        });
    });
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
