import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { IAuthenticateUser, IUser } from '../models/user';
import { FormService } from '../services/form.service';
import { ConfirmEmailService } from '../services/confirm-email.service';
import { EnvService } from '../services/env.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  formService: FormService;
  auth_google_url = this._env.APIOption.OAuthGoogleEndpoint;
  
  constructor(private _userService: UserService,
    private _emailService: ConfirmEmailService,
    private _env: EnvService,
    private _router: Router) {
    this.login  = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      remember: new FormControl('')
    });
    this.formService = new FormService(this.login);
  }

  ngOnInit(): void { }

  submit() {    
    if(this.login.valid) {
      const data = new FormData();
      data.append('email', this.login.value.email);
      data.append('password', this.login.value.password);
      this._userService.login(data).subscribe({
        next: (user: IAuthenticateUser) => {
          this._userService.setCurrentUser(user);
          this._router.navigateByUrl('/main/plans');
        },
        error: (err: HttpErrorResponse) => {
          if(err.status == 401)
            this._router.navigateByUrl(`/email_confirm/4?uid=${err.error.id}`);
        }
      });
    }
  }

  forgotPwd() {
    const email = prompt("Please enter your email");
    if(email) {
      this._emailService.sendChangePasswordConfirmEmail(email).subscribe(() => this._router.navigateByUrl(`/forgot_password/4?email=${email}`));
    }      
  }
}
