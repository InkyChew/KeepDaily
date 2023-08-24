import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlansComponent } from './plans/plans.component';
import { PlanComponent } from './plan/plan.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { LandingComponent } from './landing/landing.component';
import { EmailConfirmComponent } from './email-confirm/email-confirm.component';

const routes: Routes = [
  {path: '', component: LandingComponent, children: [
    {path:'', redirectTo: 'login', pathMatch: 'full'},
    {path: 'login', component: LoginComponent, pathMatch: 'full'},
    {path: 'register', component: RegisterComponent, pathMatch: 'full'},
  ]},
  {path: 'email_confirm/:status', component: EmailConfirmComponent, pathMatch: 'full'},
  {path: 'plans', component: PlansComponent, pathMatch: 'full'},
  {path: 'plan/:id', component: PlanComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
