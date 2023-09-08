import { Component, OnInit } from '@angular/core';
import { HelperService, MsgModal, MsgType } from '../services/helper.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-msg-modal',
  templateUrl: './msg-modal.component.html',
  styleUrls: ['./msg-modal.component.css']
})
export class MsgModalComponent implements OnInit {

  msg$: Observable<MsgModal | undefined> = this._helperService.msg$;
  
  constructor(private _helperService: HelperService) { }

  ngOnInit(): void { }

  color(type: MsgType) {
    switch(type) {
      case MsgType.Info:
        return 'var(--info)';
      case MsgType.Warning:
        return 'var(--warning)';
      case MsgType.Error:
        return 'var(--danger)'; 
    }
  }

  close() {
    this._helperService.msg$.next(undefined);
  }
}
