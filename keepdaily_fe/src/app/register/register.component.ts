import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { FormService } from '../services/form.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  register: FormGroup;
  formService: FormService;
  
  constructor(private _userService: UserService,
    private _router: Router) {
    this.register  = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmpassword: new FormControl('', [Validators.required])
    });
    this.formService = new FormService(this.register);
    this.register.addValidators(this.formService.passwordMatchValidation())
  }

  ngOnInit(): void { }

  submit() {
    if(this.register.valid) {
      const data = new FormData();
      data.append('name', this.register.value.name);
      data.append('email', this.register.value.email);
      data.append('password', this.register.value.password);
      this._userService.register(data).subscribe((user) => {
        this._router.navigateByUrl(`/email_confirm/4?uid=${user.id}}]`);
      });
    }
  }
}
