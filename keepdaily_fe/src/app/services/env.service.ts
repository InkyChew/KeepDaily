import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvService {

  constructor() { }

  get APIOption(): IRestAPIOption {
    return {
      UserEndpoint: `${environment.api.url}${environment.api.endpoint.user}`,
      PlanEndpoint: `${environment.api.url}${environment.api.endpoint.plan}`,
      DayEndpoint: `${environment.api.url}${environment.api.endpoint.day}`,
      LineNotifyEndpoint: `${environment.api.url}${environment.api.endpoint.lineNotify}`,
      ConfirmEmailEndpoint: `${environment.api.url}${environment.api.endpoint.confirmEmail}`
    }
  }
}

export interface IRestAPIOption {
  UserEndpoint: string,
  PlanEndpoint: string,
  DayEndpoint: string,
  LineNotifyEndpoint: string,
  ConfirmEmailEndpoint: string
}