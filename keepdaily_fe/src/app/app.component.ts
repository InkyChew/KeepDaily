import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';
import { HubService } from './services/hub.service';
import { concatMap } from 'rxjs';
import { IMessage } from './models/message';
import { MessageService } from './services/message.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLogin: boolean = false;
  msgList: IMessage[] = [];
  showMsg: boolean = false;

  constructor(private _userService: UserService,
    private _router: Router,
    private _msgService: MessageService,
    private _hubService: HubService) {}

  ngOnInit() {
    this._userService.user$.subscribe(user => {
      this.isLogin = user ? true : false;
      if(user && this.isLogin) {
        this._router.navigateByUrl('/main');
        this._msgService.getMessages(user.id).subscribe(msgs => this.msgList = msgs);
        this._hubService.startConnection()
        .pipe(
          concatMap(() => this._hubService.setUserIdentifier(`${user.id}`)),
          concatMap(() => this._hubService.onMessageReceived())
        ).subscribe(msg => {
          this.msgList.unshift(msg);
        });
      } else this._hubService.stopConnection();
    });
  }

  logout() {
    this._userService.logout();
  }

  ngOnDestroy() {
    this._hubService.stopConnection();
  }
}
