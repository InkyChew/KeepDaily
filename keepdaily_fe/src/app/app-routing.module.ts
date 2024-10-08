import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlansComponent } from './plans/plans.component';
import { PlanComponent } from './plan/plan.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { LandingComponent } from './landing/landing.component';
import { EmailConfirmComponent } from './email-confirm/email-confirm.component';
import { UserSettingComponent } from './user-setting/user-setting.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { SettingCategoryComponent } from './setting-category/setting-category.component';
import { MainComponent } from './main/main.component';
import { FriendsComponent } from './friends/friends.component';
import { FriendPlansComponent } from './friend-plans/friend-plans.component';
import { FriendComponent } from './friend/friend.component';
import { VideoModalComponent } from './video-modal/video-modal.component';
import { AuthGuard } from './helps/auth.guard';
import { UserLevel } from './models/user';
import { AboutComponent } from './about/about.component';

const routes: Routes = [
  {path: '', component: LandingComponent, children: [
    {path:'', redirectTo: 'login', pathMatch: 'full'},
    {path: 'login', component: LoginComponent, pathMatch: 'full'},
    {path: 'register', component: RegisterComponent, pathMatch: 'full'},
  ]},
  {path: 'about', component: AboutComponent, pathMatch: 'full'},
  {path: 'email_confirm/:status', component: EmailConfirmComponent, pathMatch: 'full'},
  {path: 'forgot_password/:status', component: ForgotPasswordComponent, pathMatch: 'full'},
  {path: 'user_setting', component: UserSettingComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  {path: 'main', component: MainComponent, canActivate:[AuthGuard], children: [
    {path:'', redirectTo: 'plans', pathMatch: 'full'},
    {path: 'plans', component: PlansComponent, pathMatch: 'full'},
    {path: 'friends', component: FriendsComponent, pathMatch: 'full'}
  ]},
  {path: 'friend/:fid', component: FriendComponent, children: [
    {path:'', redirectTo: 'plans', pathMatch: 'full'},
    {path: 'plans', component: FriendPlansComponent, pathMatch: 'full'},
    {path: 'friends', component: FriendsComponent, pathMatch: 'full'}
  ]},
  {path: 'plan/:id', component: PlanComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  {path: 'plan/:id/video', component: VideoModalComponent, pathMatch: 'full', canActivate:[AuthGuard]},
  {path: 'setting_category', component: SettingCategoryComponent, pathMatch: 'full', canActivate:[AuthGuard], data: {role: UserLevel.Admin}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
