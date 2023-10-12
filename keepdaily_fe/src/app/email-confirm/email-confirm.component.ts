import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfirmEmailService } from '../services/confirm-email.service';
import { TranslateService } from '../services/translate.service';

@Component({
  selector: 'app-email-confirm',
  templateUrl: './email-confirm.component.html',
  styleUrls: ['./email-confirm.component.css']
})
export class EmailConfirmComponent implements OnInit {

  // 1:success, 2:user not exist, 3:email fail to confirm, 4:resend
  status: number = +this._route.snapshot.params['status'];
  userId: number = +this._route.snapshot.queryParams['uid'];
  email: string = this._route.snapshot.queryParams['email'];
  lang: string = this._translateService.defaultLang;

  constructor(private _route: ActivatedRoute,
    private _service: ConfirmEmailService,
    private _translateService: TranslateService) {
    this._translateService.curLang$.subscribe(lang => this.lang = lang);
  }

  ngOnInit(): void { }

  send() {
    if(this.userId) {
      this._service.sendConfirmEmail(this.userId).subscribe(() => this.status = 4);
    }
  }
}
