import { Component, OnInit } from '@angular/core';
import { Calendar, Plan } from '../models/calendar';
import { HelperService } from '../services/helper.service';
import { PlanService } from '../services/plan.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-plan',
  templateUrl: './plan.component.html',
  styleUrls: ['./plan.component.css']
})
export class PlanComponent implements OnInit {

  planId: number = 0;
  plan?: Plan;
  year?: number;
  month?: number;
  calendar?: Calendar;
  editable: boolean = false;
  monthList = this._helper.Month;

  constructor(private _helper: HelperService,
    private _service: PlanService,
    private _userService: UserService,
    private _route: ActivatedRoute,
    private _router: Router) { }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.planId = params['id'];
      if(this.planId) this.getPlan();
      else this.goBack();
    });
  }

  getPlan() {
    this._service.getPlan(this.planId, this.year, this.month).subscribe(res => {
      this.editable = res.userId == this._userService.getCurrentUser().id;
      this.plan = res;
      if(!this.year) this.year = new Date(this.plan.updateTime).getFullYear();
      if(!this.month) this.month = new Date(this.plan.updateTime).getMonth();
      
      this.calendar = new Calendar(this.year, this.month, this.plan.days);
    });
  }

  change(type: string, e:any) {
    const v = +(e.target.value);
    
    if(type == 'year') this.year = v;
    if(type == 'month') this.month = v;
    this.getPlan();
  }

  goBack() {
    this._router.navigateByUrl('/main/plans');
  }
}
