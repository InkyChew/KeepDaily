import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';
import { HubService } from './services/hub.service';
import { concatMap } from 'rxjs';
import { IMessage } from './models/message';
import { MessageService } from './services/message.service';
import { IAuthenticateUser } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  
  isLogin: boolean = false;
  msgList: IMessage[] = [];
  newmsgs: IMessage[] = [];
  showMsg: boolean = false;

  constructor(private _userService: UserService,
    private _router: Router) {}

  ngOnInit() {
    this._userService.user$.subscribe(user => {
      this.isLogin = user ? true : false;
      if(this.isLogin) {
        this._router.navigateByUrl('/main');
      }
    });
  }

  logout() {
    this._userService.logout();
  }
}
