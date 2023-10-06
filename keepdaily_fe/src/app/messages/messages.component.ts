import { Component, Input, OnInit } from '@angular/core';
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

  constructor() { }

  ngOnInit(): void { }

  getImg(img: string) {
    return `${environment.api.url}/${img}`
  }

  // click btn then isRead = true
  // if empty show no messages
  // if isRead = false, count and show number of new messages 
}
