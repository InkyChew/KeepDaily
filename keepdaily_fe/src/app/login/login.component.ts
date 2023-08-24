import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  
  constructor(private _userService: UserService,
    private _router: Router) {
    this.login  = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      pwd: new FormControl('', [Validators.required]),
      remember: new FormControl('')
    });
  }

  ngOnInit(): void { }

  getErrorMsg(field: string) {
    let msg = '';
    if(field) {
      const control: FormControl = (this.login.controls as any)[field];      
      if(control.dirty) {
        if(control.hasError('required'))
          msg = `${field.charAt(0).toUpperCase()}${field.slice(1)} is required`;
      }
    }
    return msg;
  }

  submit() {    
    if(this.login.valid) {
      const data = new FormData();
      data.append('email', this.login.value.email);
      data.append('password', this.login.value.pwd);
      this._userService.login(data).subscribe({
        next: () => {
          this._router.navigateByUrl('/plans');
        },
        error: (err: HttpErrorResponse) => {
          console.log(err);
          
          if(err.status == 405)
            this._router.navigateByUrl(`/email_confirm/4?uid=${err.error}`);
        }
      });
    }
  }
}
