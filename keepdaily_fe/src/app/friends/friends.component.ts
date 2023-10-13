import { Component, OnInit } from '@angular/core';
import { IUser } from '../models/user';
import { FriendService } from '../services/friend.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IUserQuery, UserService } from '../services/user.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

  uid = this._userService.getCurrentUser().id;
  friendList: IUser[] = [];
  searchFriends?: IUser[];

  constructor(private _service: FriendService,
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _router: Router) { }

  ngOnInit(): void {
    // main
    if(this._route.parent?.snapshot.url[0].path == 'main')
      if(this.uid) this.getFriends(this.uid);
    
    // friend
    this._route.parent?.params.subscribe(params => {
      const fid = params['fid'];
      if(fid) this.getFriends(fid);
    })
  }

  getFriends(uid: number) {    
    this._service.getAllFriend(uid).subscribe(res => this.friendList = res);
  }

  getPhoto(user: IUser) {
    return this._userService.getPhoto(user.imgName, user.imgType);
  }
  
  search(qry: HTMLInputElement) {
    const emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    const isEmailValid = qry.value.match(emailRegex);
    const query: IUserQuery = isEmailValid ? { email: qry.value } : { name: qry.value };
    if(qry)
      this._userService.getAllUser(query).subscribe((res) => {
        this.searchFriends = res.filter(_ => _.id != this.uid && _.email != 'a@a');
      });
  }

  goToUserPage(id: number) {
    if(id === this.uid) this._router.navigateByUrl('main');
    else this._router.navigateByUrl(`/friend/${id}`);
  }
}
