import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Calendar, Day } from '../models/calendar';
import { DayService } from '../services/day.service';
import { HelperService } from '../services/helper.service';
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

  constructor(private _dayService: DayService,
    private _helpService: HelperService,
    private _cdr: ChangeDetectorRef) {}
  
  ngOnInit(): void {}

  canUpload(day: Day) {
    const today = new Date();
    return this.editable && !this.fileUploading && (day.year == today.getFullYear() && day.month == today.getMonth()+1 && day.date == today.getDate());
  }

  getImg(day: Day) {
    return (day.imgName && day.imgType)
            ? this._dayService.getDayImage(day.imgName, day.imgType) : void(0);
  }

  resizeFile(e: any, day: Day, i: number) {
    this._cdr.detach();
    const file = e.target.files[0];
    const canvas = document.createElement("canvas");
    const ctx = canvas.getContext("2d");

    const img = new Image();
    img.onload = () => {
      const maxWidth = 2048; // Set your desired max width
      const maxHeight = 2048; // Set your desired max height
      let newWidth = img.width;
      let newHeight = img.height;

      // Calculate the new dimensions while maintaining aspect ratio
      if (img.width > maxWidth) {
        newWidth = maxWidth;
        newHeight = Math.floor((img.height * maxWidth) / img.width);
      }

      if (newHeight > maxHeight) {
        newHeight = maxHeight;
        newWidth = Math.floor((img.width * maxHeight) / img.height);
      }

      // Resize the canvas to the new dimensions
      canvas.width = newWidth - (newWidth % 2);
      canvas.height = newHeight - (newHeight % 2);

      // Draw the image on the canvas with the new dimensions
      ctx?.drawImage(img, 0, 0, newWidth, newHeight);
      canvas.toBlob((blob: any) => {
        let resizedFile  = new File([blob], file.name, { type: file.type });
        this.uplaodFile(resizedFile, day, i);
        e.target.value = null;
      }, file.type);
    };
    img.src = URL.createObjectURL(file);
  }

  uplaodFile(file: any, day: Day, i: number) {
    if(this.canUpload(day)) {
      this.fileUploading = true;
      const data = new FormData();
      const ext = file.name.split('.')[1];
      const dt = formatDate(new Date(day.year, day.month-1, day.date), 'yy_MM_dd', 'en-US');
      day.imgName = `${day.planId}_${dt}.${ext}`;
      day.imgType = file.type;

      data.append('file', file);
      data.append('data', JSON.stringify(day));

      this._dayService.postDay(data).subscribe(res => {
        this.calendar.days[i] = {...res};
        this.fileUploading = false;
        this._cdr.detectChanges();
      });
    }
    else this._helpService.showMsg("Please select an image to upload.");
  }

  delete(day: Day, i: number) {
    this._dayService.deleteDay(day.id).subscribe(() => {
      this.calendar.days[i] = {...day, id: 0, imgName: null, imgType: null};
      this._cdr.detectChanges();
    });
  }
}