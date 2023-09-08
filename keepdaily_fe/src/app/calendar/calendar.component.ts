import { Component, Input, OnInit } from '@angular/core';
import { Calendar, Day } from '../models/calendar';
import { DayService } from '../services/day.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @Input() calendar!: Calendar;
  @Input() editable: boolean = false;
  fileUploading: boolean = false;

  constructor(private _dayService: DayService) {}
  
  ngOnInit(): void {}

  ngAfterContentInit() {
    console.log(this.calendar);
    
  }

  canUpload(date: number) {
    const today = new Date();
    return (this.calendar.year == today.getFullYear() && this.calendar.month == today.getMonth()+1 && date == today.getDate());
  }

  getImg(day: Day) {
    return `https://localhost:5000/api/Day/Img?name=${day.imgName}&type=${day.imgType}`;
  }

  uploadFile(e: any, day: Day) {
    this.fileUploading = true;
    const data = new FormData();
    const file = e.target.files[0];
    const ext = file.name.split('.')[1];
    const dt = formatDate(new Date(day.year, day.month-1, day.date), 'yy_MM_dd', 'en-US');
    day.imgName = `${day.planId}_${dt}.${ext}`;
    day.imgType = file.type;

    data.append('file', file);
    data.append('data', JSON.stringify(day));

    this._dayService.postDay(data).subscribe(res => {
      day.id = res.id;      
      this.fileUploading = false;
    });
  }

  delete(day: Day) {
    this._dayService.deleteDay(day.id).subscribe(() => {
      day.id = 0;
      day.imgName = undefined;
      day.imgType = undefined;
    });
  }
}