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

  @ViewChild('msgitem') msgitem!: ElementRef;
  user!: IAuthenticateUser;
  isLogin: boolean = false;
  msgList: IMessage[] = [];
  newmsgs: IMessage[] = [];
  showMsg: boolean = false;

  constructor(private _userService: UserService,
    private _router: Router,
    private _msgService: MessageService,
    private _hubService: HubService) {}

  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    const target = event.target as HTMLElement;
    const msgEl = this.msgitem?.nativeElement;

    if (this.showMsg && !msgEl?.contains(target)) {
      this.showMsg = false;
      this.updateReadMsg();
    }
  }

  ngOnInit() {
    this._userService.user$.subscribe(user => {
      this.isLogin = user ? true : false;
      if(user && this.isLogin) {
        this.user = user;
        this._router.navigateByUrl('/main');

        this.getMsgList();

        // Msg Listener
        this._hubService.startConnection()
        .pipe(
          concatMap(() => this._hubService.setUserIdentifier(`${user.id}`)),
          concatMap(() => this._hubService.onMessageReceived())
        ).subscribe(msg => {
          this.msgList.unshift(msg);
          this.filterNewMsg();
        });
      } else this._hubService.stopConnection();
    });
  }
  
  getMsgList() {
    this._msgService.getMessages(this.user.id).subscribe(msgs => {
      this.msgList = msgs;
      this.filterNewMsg();
    });
  }

  filterNewMsg() {
    this.newmsgs = this.msgList.filter(_ => !_.isRead);
  }

  updateReadMsg() {
    if(this.newmsgs.length > 0)
      this._msgService.updateReadMessage(this.newmsgs).subscribe(() => this.getMsgList());
  }

  openMsgBox() {
    if(this.showMsg) this.updateReadMsg();
    this.showMsg = !this.showMsg;
  }

  deleteMsg(id: number) {
    this._msgService.deleteMessage(id).subscribe(() => this.getMsgList());
  }

  logout() {
    this._userService.logout();
  }

  ngOnDestroy() {
    this._hubService.stopConnection();
  }
}
