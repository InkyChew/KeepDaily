import { Component, OnInit } from '@angular/core';
import { IUser } from '../models/user';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  user?: IUser;
  constructor(private _router: Router,
    private _userService: UserService) { }

  ngOnInit(): void {
    const uid = this._userService.getCurrentUser().id;
    this.getUser(uid);
  }

  getUser(id: number) {
    this._userService.getUser(id).subscribe({
      next: res => this.user = res,
      error: err => {
        if(err.status == 404) this.goToLogin();
      }
    });
  }
  
  getPhoto(user: IUser) {
    return this._userService.getPhoto(user.imgName, user.imgType);
  }
  
  goToLogin() {
    this._router.navigateByUrl('/login');
  }
}
