import { Component, OnInit } from '@angular/core';
import { PlanService } from '../services/plan.service';
import { Plan } from '../models/calendar';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-friend-plans',
  templateUrl: './friend-plans.component.html',
  styleUrls: ['../plans/plans.component.css']
})
export class FriendPlansComponent implements OnInit {
  
  planList: Plan[] = [];
  
  constructor(private _service: PlanService,
    private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.parent?.params.subscribe(params => {
      const id = params['fid'];
      console.log(id);
      if(id) this.getPlans(id);
    });
  }

  getPlans(id: number) {
    this._service.getAllPlan(id).subscribe(res => this.planList = res);
  }
}
