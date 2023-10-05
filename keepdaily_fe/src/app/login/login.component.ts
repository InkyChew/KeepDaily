import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { IAuthenticateUser } from '../models/user';
import { FormService } from '../services/form.service';
import { ConfirmEmailService } from '../services/confirm-email.service';
import { EnvService } from '../services/env.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @ViewChild('gbtn') gbtn: ElementRef = new ElementRef({});
  login: FormGroup;
  formService: FormService;
  auth_google_url: string = this._env.APIOption.OAuthGoogleEndpoint;
  
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
  
  // ngAfterViewInit(): void {
  //   // this.loadGoogleSignInScript(() => this.auth_google_url = this._env.APIOption.OAuthGoogleEndpoint)
  //   this.loadGoogleSignInScript(() => {
  //     // @ts-ignore
  //     window.google.accounts.id.initialize({
  //       client_id: "272976394573-m3k7tm2m60a4dkm6qj8mlqn79dh48ckm.apps.googleusercontent.com",
  //       context: "signin",
  //       ux_mode: "popup",
  //       login_uri: this._env.APIOption.OAuthGoogleEndpoint,
  //       itp_support: true
  //     });
  //     // @ts-ignore
  //     window.google.accounts.id.renderButton(
  //       // @ts-ignore
  //       this.gbtn.nativeElement,
  //       { type: "standard",
  //         shape: "rectangular",
  //         theme: "outline",
  //         text: "signin_with",
  //         size: "large",
  //         logo_alignment: "left"
  //       }
  //     );
  //   });
  // }

  private loadGoogleSignInScript(callback: () => void) {
    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    script.onload = callback;

    // Append the script to the document
    document.head.appendChild(script);
  }

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
