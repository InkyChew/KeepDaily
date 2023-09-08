import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { Friend, IUser } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAllFriend(uid: number) {
    return this._http.get<Array<IUser>>(`${this._env.APIOption.FriendEndpoint}/${uid}`);
  }

  getFriend(uid: number, fid: number) {
    return this._http.get<Friend>(`${this._env.APIOption.FriendEndpoint}?uid=${uid}&fid=${fid}`);
  }

  postFriend(friend: Friend) {
    return this._http.post(this._env.APIOption.FriendEndpoint, friend);
  }

  deleteFriend(friend: Friend) {
    return this._http.delete(this._env.APIOption.FriendEndpoint, {body: friend});
  }
}
