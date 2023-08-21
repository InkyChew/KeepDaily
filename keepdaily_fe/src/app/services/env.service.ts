import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvService {

  constructor() { }

  get APIOption(): IRestAPIOption {
    return {
      PlanEndpoint: `${environment.api.url}${environment.api.endpoint.plan}`,
      LineNotifyEndpoint: `${environment.api.url}${environment.api.endpoint.lineNotify}`
    }
  }
}

export interface IRestAPIOption {
  PlanEndpoint: string,
  LineNotifyEndpoint: string
}