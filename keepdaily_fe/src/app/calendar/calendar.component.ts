import { Component, Input, OnInit } from '@angular/core';
import { Calendar, Day } from '../models/calendar';
import { DayService } from '../services/day.service';
import { formatDate } from '@angular/common';
import { HelperService } from '../services/helper.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @Input() calendar!: Calendar;
  @Input() editable: boolean = false;
  fileUploading: boolean = false;

  constructor(private _dayService: DayService,
    private _helpService: HelperService) {}
  
  ngOnInit(): void {}

  canUpload(day: Day) {
    const today = new Date();
    return this.editable && (day.year == today.getFullYear() && day.month == today.getMonth()+1 && day.date == today.getDate());
  }

  getImg(day: Day) {
    return (day.imgName && day.imgType)
            ? this._dayService.getDayImage(day.imgName, day.imgType) : void(0);
  }

  uploadFile(e: any, day: Day) {    
    const MediaTypePattern = /^image\/.*$/;
    const file = e.target.files[0];
    if(this.canUpload(day) && MediaTypePattern.test(file.type)) {
      this.fileUploading = true;
      const data = new FormData();
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
    else this._helpService.showMsg("Please select an image to upload.");
    
    e.target.value = null;
  }

  delete(day: Day) {
    this._dayService.deleteDay(day.id).subscribe(() => {
      day.id = 0;
      day.imgName = undefined;
      day.imgType = undefined;
    });
  }
}