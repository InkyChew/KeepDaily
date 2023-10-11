import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { IMessage } from '../models/message';
import { environment } from 'src/environments/environment';
import { MessageService } from '../services/message.service';
import { HubService } from '../services/hub.service';
import { UserService } from '../services/user.service';
import { IAuthenticateUser } from '../models/user';
import { concatMap } from 'rxjs';
@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  user!: IAuthenticateUser;
  msgList: IMessage[] = [];
  newmsgs: IMessage[] = [];
  show: boolean = false;

  constructor(private elementRef: ElementRef,
    private _userService: UserService,
    private _msgService: MessageService,    
    private _hubService: HubService) { }

  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    const target = event.target as HTMLElement;
    const msgEl = this.elementRef.nativeElement;

    if (this.show && !msgEl?.contains(target)) {
      this.show = false;
      this.updateReadMsg();
    }
  }

  ngOnInit(): void {
    this._userService.user$.subscribe(user => {
      if(user) {
        this.user = user;

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

  getImg(img: string) {
    return `${environment.api.url}/${img}`
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
    if(this.show) this.updateReadMsg();
    this.show = !this.show;
  }

  deleteMsg(id: number) {
    this._msgService.deleteMessage(id).subscribe(() => this.getMsgList());
  }

  ngOnDestroy() {
    this._hubService.stopConnection();
  }
}
