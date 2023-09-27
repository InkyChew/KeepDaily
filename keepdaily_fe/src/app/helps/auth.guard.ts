import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { UserLevel } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private _userService: UserService,
    private _router: Router) {}
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const roleAllowed = route.data["role"] ?? UserLevel.General;
      const user = this._userService.getCurrentUser();
      
      if(user && user.id && user.userLevel == roleAllowed) return true;
      else {
        this._router.navigateByUrl('/login');
        return false;
      }
  }
  
}
