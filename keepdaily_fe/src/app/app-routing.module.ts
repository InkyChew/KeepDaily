import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlansComponent } from './plans/plans.component';
import { PlanComponent } from './plan/plan.component';

const routes: Routes = [
  {path: 'plans', component: PlansComponent, pathMatch: 'full'},
  {path: 'plan/:id', component: PlanComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
