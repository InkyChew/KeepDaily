import { Component, OnInit } from '@angular/core';
import { FormService } from '../services/form.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmEmailService } from '../services/confirm-email.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  // 1:success, 2:user not exist, 3:email fail to confirm, 4:resend
  status: number = +this._route.snapshot.params['status'];
  userId: number = +this._route.snapshot.queryParams['uid'];
  email: string = this._route.snapshot.queryParams['email'];
  changePwd: FormGroup;
  formService: FormService;
  
  constructor(private _userService: UserService,
    private _emailService: ConfirmEmailService,
    private _route: ActivatedRoute,
    private _router: Router) {
    this.changePwd  = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirmpassword: new FormControl('', [Validators.required])
    });
    this.formService = new FormService(this.changePwd);
    this.changePwd.addValidators(this.formService.passwordMatchValidation())
  }

  ngOnInit(): void { }

  submit() {
    if(this.changePwd.valid) {
      this._userService.updatePassword(this.userId, this.changePwd.value.password).subscribe(() => {
        this._router.navigateByUrl(`/login`);
      });
    }
  }

  send() {
    if(this.email)
      this._emailService.sendChangePasswordConfirmEmail(this.email).subscribe(() => this.status = 4);
  }
}
