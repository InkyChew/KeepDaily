import { Component, OnInit } from '@angular/core';
import { ICategory, Plan } from '../models/calendar';
import { PlanService } from '../services/plan.service';
import { formatDate } from '@angular/common';
import { IUser } from '../models/user';
import { CategoryService } from '../services/category.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {

  user: IUser = this._userService.getCurrentUser();
  planList: Plan[] = [];
  ctgList: ICategory[] = [];
  edit: number = -1;
  today: string = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');

  constructor(private _service: PlanService,
    private _ctgService: CategoryService,
    private _userService: UserService) { }

  ngOnInit(): void {
    this.getPlans();
    this.getCtgList();
  }

  getPlans() {
    this._service.getAllPlan(this.user.id).subscribe(res => this.planList = res);
  }

  getCtgList() {
    this._ctgService.getAllCategory().subscribe(res => this.ctgList = res);
  }

  add() {
    this.planList.unshift(new Plan(this.user.id, this.today));
    this.edit = 0;
  }

  save(plan: Plan, i: number) {
    const method = plan.id == 0
            ? this._service.postPlan(plan)
            : this._service.putPlan(plan);
    method.subscribe(res => {
      this.planList[i] = res;
      this.closeEditor();
    });
  }

  delete(id: number, i: number) {
    this._service.deletePlan(id).subscribe(() => this.planList.splice(i,1));
  }

  closeEditor(plan?: Plan) {
    if(plan?.id == 0) this.planList.splice(0,1);
    this.edit = -1;
  }
}
