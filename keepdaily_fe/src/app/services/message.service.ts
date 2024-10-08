import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { HttpClient } from '@angular/common/http';
import { IMessage } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getMessages(uid: number) {
    return this._http.get<IMessage[]>(`${this._env.APIOption.MessageEndpoint}?uid=${uid}`);
  }

  updateReadMessage(newmsgs: IMessage[]) {
    return this._http.patch(this._env.APIOption.MessageEndpoint, newmsgs);
  }

  deleteMessage(id: number) {
    return this._http.delete(`${this._env.APIOption.MessageEndpoint}/${id}`);
  }
}
