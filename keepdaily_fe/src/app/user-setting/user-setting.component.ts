import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../services/user.service';
import { FormService } from '../services/form.service';
import { IUser } from '../models/user';
import { Router } from '@angular/router';
import { LineNotifyService } from '../services/line-notify.service';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.css']
})
export class UserSettingComponent implements OnInit {

  _usrimg?: ElementRef;
  @ViewChild('usrimg') 
  set usrimg(e: ElementRef) {
    this._usrimg = e;
    this.setPhoto();
  }
  info: FormGroup;
  formService: FormService;
  user!: IUser;
  
  constructor(private _router: Router,
    private _userService: UserService,
    private _lineNotifyService: LineNotifyService) {
    this.info  = new FormGroup({
      password: new FormControl(''),
      confirmpassword: new FormControl('')
    });
    this.formService = new FormService(this.info);
    this.info.addValidators(this.formService.passwordMatchValidation())
  }

  ngOnInit(): void {
    const uid = JSON.parse(localStorage.getItem("user")!)?.id;
    if(uid) this.getUser(uid);
    else this.goToLogin();
  }

  getUser(id: number) {
    this._userService.getUser(id).subscribe({
      next: res => this.user = res,
      error: err => {
        if(err.status == 404) this.goToLogin();
      }
    });
  }

  setPhoto() {
    if(this._usrimg) 
      this._usrimg.nativeElement.src = this._userService.getPhoto(this.user);
  }

  uploadPhoto(e: any) {
    const file = e.target.files[0];
    const data = new FormData();
    data.append('file', file);
    this._userService.updatePhoto(this.user.id, data).subscribe({
      next: () => {
        this.getUser(this.user.id);
        this.setPhoto();
      },
      error: () => e.target.value = null
    });
  }

  delPhoto() {
    const u = this.user;
    if(u.id && u.imgName && u.imgType) {
      this._userService.deletePhoto(u.id, u.imgName, u.imgType).subscribe(() => this.getUser(this.user.id));
    }
  }

  save() {
    if(this.info.valid) {
      if(this.info.value.password) this.user.password = this.info.value.password;

      this._userService.updateUser(this.user).subscribe((res) => this.user = res);
    }
  }

  getLineNotify() {
    this._lineNotifyService.getAuth(this.user.email).subscribe(res => window.location.href = res);
  }

  goToLogin() {
    this._router.navigateByUrl('/login');
  }
}
