import { Component, Input, OnInit } from '@angular/core';
import { Calendar, Day } from '../models/calendar';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @Input() calendar!: Calendar;

  constructor() {}
  
  ngOnInit(): void {}

  canUpload(date: number) {
    const today = new Date();
    return (this.calendar.year == today.getFullYear() && this.calendar.month == today.getMonth() && date == today.getDate());
  }

  uploadFile(e: any, day: Day) {
    const file = e.target.files[0];
    const data = this.calendar;
    data.days = [day];
    console.log(data);
  }
}