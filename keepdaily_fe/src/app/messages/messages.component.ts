import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IMessage } from '../models/message';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  @Input() messages: IMessage[] = [];
  @Input() show: boolean = false;
  @Output() onDeleteMsg = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void { }

  getImg(img: string) {
    return `${environment.api.url}/${img}`
  }

  delMsg(id: number) {
    this.onDeleteMsg.emit(id);
  }
}
