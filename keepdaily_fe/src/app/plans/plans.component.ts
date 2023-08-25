import { Component, OnInit } from '@angular/core';
import { Plan } from '../models/calendar';
import { PlanService } from '../services/plan.service';
import { formatDate } from '@angular/common';
import { IUser } from '../models/user';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {

  user: IUser = JSON.parse(localStorage.getItem('user')!);
  planList: Plan[] = [];
  edit: number = -1;
  today: string = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');

  constructor(private _service: PlanService) { }

  ngOnInit(): void {
    this.getPlans();
  }

  getPlans() {
    this._service.getAllPlan().subscribe(res => this.planList = res);
  }

  add() {
    this.planList.unshift(new Plan(this.today));
    this.edit = 0;
  }

  save(plan: Plan) {
    if(plan.id == 0) {
      this._service.postPlan(plan).subscribe(res => {
        this.planList[0] = res;
        this.closeEditor();
      });
    } else this._service.putPlan(plan).subscribe(() => this.closeEditor());
  }

  delete(id: number, i: number) {
    this._service.deletePlan(id).subscribe(() => this.planList.splice(i,1));
  }

  closeEditor(plan?: Plan) {
    if(plan?.id == 0) this.planList.splice(0,1);
    this.edit = -1;
  }
}
