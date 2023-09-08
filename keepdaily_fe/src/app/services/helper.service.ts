import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  Month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
  readonly msg$ = new BehaviorSubject<MsgModal | undefined>(undefined);
  readonly loader$ = new BehaviorSubject<boolean>(false);

  constructor() { }

  showMsg(text: string, type?: MsgType) {
    const msg = new MsgModal(text, type);
    this.msg$.next(msg);
  }

  startLoading() {
    this.loader$.next(true);
  }

  stopLoading() {
    this.loader$.next(false);
  }
}

export class MsgModal {
  type: MsgType = MsgType.Error;
  text: string;

  constructor(txt: string, type?: MsgType) {
    this.text = txt; 
  }
}

export enum MsgType {
  Info,
  Warning,
  Error,
}
