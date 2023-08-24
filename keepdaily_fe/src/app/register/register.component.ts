import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  register: FormGroup;
  
  constructor(private _userService: UserService,
    private _router: Router) {
    this.register  = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      pwd: new FormControl('', [Validators.required]),
      cfpwd: new FormControl('', [Validators.required])
    }, {validators: this.passwordMatchValidation()});
  }

  ngOnInit(): void { }

  passwordMatchValidation(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const pwd = control.get('pwd');
      const cpwd = control.get('cfpwd');

      return (pwd?.dirty && cpwd?.dirty && pwd.value != cpwd.value)
      ? {passwordMismatch: true} : null;
    }
  }

  getErrorMsg(field: string) {
    let msg = '';
    if(field) {
      const control: FormControl = (this.register.controls as any)[field];      
      if(control.dirty) {
        if(control.hasError('required'))
          msg = `${field.charAt(0).toUpperCase()}${field.slice(1)} is required`;
        else if((field == 'pwd' || field == 'cfpwd') && this.register.hasError('passwordMismatch'))
          msg = `Password and Confirm Password should be the same.`;
      }
    }
    return msg;
  }

  submit() {    
    if(this.register.valid) {
      const data = new FormData();
      data.append('name', this.register.value.name);
      data.append('email', this.register.value.email);
      data.append('password', this.register.value.pwd);
      this._userService.register(data).subscribe((user) => {
        this._router.navigateByUrl(`/email_confirm/4?uid=${user.id}}]`);
      });
    }
  }
}
