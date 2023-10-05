import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLogin: boolean = false;

  constructor(private _userService: UserService,
    private _router: Router) {}

  ngOnInit() {
    this._userService.user$.subscribe(res => {
      this.isLogin = res ? true : false;
      if(this.isLogin) this._router.navigateByUrl('/main');
    });
  }

  logout() {
    this._userService.logout();
  }
}
