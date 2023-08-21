import { Component, OnInit } from '@angular/core';
import { Calendar, Day, Plan } from '../models/calendar';
import { HelperService } from '../services/helper.service';
import { LineNotifyService } from '../services/line-notify.service';

@Component({
  selector: 'app-plan',
  templateUrl: './plan.component.html',
  styleUrls: ['./plan.component.css']
})
export class PlanComponent implements OnInit {

  date = new Date();
  year = this.date.getFullYear();
  month = this.date.getMonth();
  today = this.date.getDate();
  plan: Plan = new Plan();
  calendar: Calendar;  
  monthList = this._helper.Month;

  constructor(private _helper: HelperService, private _lineNotifyService: LineNotifyService) {
    this.calendar = (this.plan.calendars.length > 0) ? this.plan.calendars[0] : new Calendar(this.year, this.month);
    this.createDayList();
  }

  ngOnInit(): void {}

  createDayList(year: number = this.year, month: number = this.month) {
    this.calendar.days = [];
    const lastdate = new Date(year, month + 1, 0).getDate();
    const prelastdate = new Date(year, month, 0).getDate();
    const firstday = new Date(year, month, 1).getDay();
    const lastday = new Date(year, month, lastdate).getDay();

    for(let i = firstday; i > 0; i--)
      this.calendar.days.push(new Day(prelastdate - i + 1));
    
    for(let i = 1; i <= lastdate; i++)
      this.calendar.days.push(new Day(i));

    for(let i = lastday; i < 6; i++)
      this.calendar.days.push(new Day(i - lastday + 1));      
  }

  change(type: string, e:any) {
    const v = +(e.target.value);
    console.log(v);
    
    if(type == 'year') this.year = v;
    if(type == 'month') this.month = v;
    this.createDayList();
  }

  GetLineNotify() {
    this._lineNotifyService.getAuth().subscribe(res => {
      console.log(res);
      
      window.location.href = res;
    })
  }
}
