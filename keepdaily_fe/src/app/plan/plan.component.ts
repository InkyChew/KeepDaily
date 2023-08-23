import { Component, OnInit } from '@angular/core';
import { Calendar, Day, Plan } from '../models/calendar';
import { HelperService } from '../services/helper.service';
import { LineNotifyService } from '../services/line-notify.service';
import { formatDate } from '@angular/common';
import { PlanService } from '../services/plan.service';
import { ActivatedRoute, Router } from '@angular/router';

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
  monthList = this._helper.Month;

  constructor(private _helper: HelperService,
    private _service: PlanService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _lineNotifyService: LineNotifyService) { }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.planId = params['id'];
      if(this.planId) this.getPlan();
      else this.goBack();
    });
  }

  getPlan() {
    this._service.getPlan(this.planId, this.year, this.month).subscribe(res => {
      this.plan = res;
      const sf = this.plan.startFrom.split('-');
      if(!this.year) this.year = parseInt(sf[0]);
      if(!this.month) this.month = parseInt(sf[1]);      
      this.calendar = new Calendar(this.year, this.month, this.plan.days);
    });
  }

  change(type: string, e:any) {
    const v = +(e.target.value);
    console.log(v);
    
    if(type == 'year') this.year = v;
    if(type == 'month') this.month = v;
    this.getPlan();
  }

  GetLineNotify() {
    this._lineNotifyService.getAuth().subscribe(res => window.location.href = res)
  }

  goBack() {
    this._router.navigateByUrl('/plans')
  }
}
