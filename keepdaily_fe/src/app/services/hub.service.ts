import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from "@microsoft/signalr";
import { EnvService } from './env.service';
import { IMessage } from '../models/message';
import { Observable, catchError, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  hubConnection = new signalR.HubConnectionBuilder()
                  .withUrl(this._env.APIOption.MessageHubEndpoint)
                  .build();

  constructor(private _http: HttpClient, private _env: EnvService) { }

  startConnection(): Observable<void> {
    return from(this.hubConnection.start()).pipe(
      catchError((error) => {
        console.error('Error starting hub connection', error);
        throw error;
      })
    );
  }

  setUserIdentifier(uid: string): Observable<void> {
    return from(this.hubConnection.invoke('SetUserIdentifier', uid)).pipe(
      catchError((error) => {
        console.error('Error invoking SetUserIdentifier', error);
        throw error;
      })
    );
  }

  onMessageReceived(): Observable<IMessage> {
    return new Observable<IMessage>((observer) => {
      this.hubConnection.on("onMessageReceived", (msg) => {
        observer.next(msg);
      });
    });
  }

  stopConnection() {
    if (this.hubConnection)
      this.hubConnection.stop();
  }
}
