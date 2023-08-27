import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalendarComponent } from './calendar/calendar.component';
import { MonthPipe } from './pipes/month.pipe';
import { PlansComponent } from './plans/plans.component';
import { PlanComponent } from './plan/plan.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { LandingComponent } from './landing/landing.component';
import { EmailConfirmComponent } from './email-confirm/email-confirm.component';
import { MainComponent } from './main/main.component';
import { UserSettingComponent } from './user-setting/user-setting.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { SettingCategoryComponent } from './setting-category/setting-category.component';
import { FriendsComponent } from './friends/friends.component';

@NgModule({
  declarations: [
    AppComponent,
    CalendarComponent,
    MonthPipe,
    PlansComponent,
    PlanComponent,
    RegisterComponent,
    LoginComponent,
    LandingComponent,
    EmailConfirmComponent,
    MainComponent,
    UserSettingComponent,
    ForgotPasswordComponent,
    SettingCategoryComponent,
    FriendsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
