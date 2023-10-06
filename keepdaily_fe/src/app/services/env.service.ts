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
      FriendEndpoint: `${environment.api.url}${environment.api.endpoint.friend}`,
      PlanEndpoint: `${environment.api.url}${environment.api.endpoint.plan}`,
      CategoryEndpoint: `${environment.api.url}${environment.api.endpoint.category}`,      
      DayEndpoint: `${environment.api.url}${environment.api.endpoint.day}`,
      LineNotifyEndpoint: `${environment.api.url}${environment.api.endpoint.lineNotify}`,
      OAuthGoogleEndpoint: `${environment.api.url}${environment.api.endpoint.oAuthGoogle}`,
      ConfirmEmailEndpoint: `${environment.api.url}${environment.api.endpoint.confirmEmail}`,
      MessageEndpoint: `${environment.api.url}${environment.api.endpoint.message}`,
      MessageHubEndpoint: `${environment.hub.url}${environment.hub.endpoint.messageHub}`
    }
  }
}

export interface IRestAPIOption {
  UserEndpoint: string,
  FriendEndpoint: string,
  PlanEndpoint: string,
  CategoryEndpoint: string,
  DayEndpoint: string,
  LineNotifyEndpoint: string,
  OAuthGoogleEndpoint: string,
  ConfirmEmailEndpoint: string,
  MessageEndpoint: string,
  MessageHubEndpoint: string
}