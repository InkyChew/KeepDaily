import { Component, OnInit } from '@angular/core';
import { FormService } from '../services/form.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  uid: number = 0;
  changePwd: FormGroup;
  formService: FormService;
  
  constructor(private _userService: UserService,
    private _route: ActivatedRoute,
    private _router: Router) {
    this.changePwd  = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirmpassword: new FormControl('', [Validators.required])
    });
    this.formService = new FormService(this.changePwd);
    this.changePwd.addValidators(this.formService.passwordMatchValidation())
  }

  ngOnInit(): void { 
    this._route.params.subscribe(params => this.uid = params['uid']);
  }

  submit() {
    if(this.changePwd.valid) {
      this._userService.updatePassword(this.uid, this.changePwd.value.password).subscribe(() => {
        this._router.navigateByUrl(`/login`);
      });
    }
  }

}
