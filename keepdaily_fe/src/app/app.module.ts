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
    EmailConfirmComponent
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
