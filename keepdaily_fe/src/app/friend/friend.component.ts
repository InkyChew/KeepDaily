import { Component, OnInit } from '@angular/core';
import { Friend, IUser } from '../models/user';
import { FriendService } from '../services/friend.service';
import { UserService } from '../services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['../main/main.component.css']
})
export class FriendComponent implements OnInit {

  friend!: IUser;
  user: IUser = this._userService.getCurrentUser();
  isFriend: boolean = false;

  constructor(private _service: FriendService,
    private _userService: UserService, private _route: ActivatedRoute) { }

  ngOnInit(): void {    
    this._route.params.subscribe(params => {
      const id = params['fid'];
      if(id) {
        this.getFriend(id);
        this.getfriendRelation(id);
      }
    })
  }

  getFriend(fid: number) {
    this._userService.getUser(fid).subscribe(res => this.friend = res);
  }
  
  getPhoto(user: IUser) {
    return this._userService.getPhoto(user);
  }

  getfriendRelation(fid: number) {
    this._service.getFriend(this.user.id, fid).subscribe(res => this.isFriend = true);
  }

  addFriend() {
    this._service.postFriend(new Friend(this.user.id, this.friend.id)).subscribe(() => this.isFriend = true);
  }

  delFriend() {
    this._service.deleteFriend(new Friend(this.user.id, this.friend.id)).subscribe(() => this.isFriend = false);
  }
}
