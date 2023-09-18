import { Component } from '@angular/core';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLogin: boolean = false;

  constructor(private _userService: UserService) {}

  ngOnInit() {
    this._userService.user$.subscribe(res => 
      this.isLogin = res ? true : false
    );
  }

  logout() {
    this._userService.logout();
  }
}
