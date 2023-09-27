import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PlanService } from '../services/plan.service';
import { Plan } from '../models/calendar';

@Component({
  selector: 'video-modal',
  templateUrl: './video-modal.component.html',
  styleUrls: ['./video-modal.component.css']
})
export class VideoModalComponent implements OnInit {

  @Input() plan!: Plan;
  @Input()year!: number;
  @Input()month!: number;
  @Output() onModalClose = new EventEmitter<boolean>();
  src?: string;

  constructor(private _service: PlanService) { }

  ngOnInit(): void {
    this.getVideo();
  }

  getVideo() {
    const monthLastDate = new Date(this.year, this.month, 0).getDate();
    const start = `${this.year}-${this.month}-${1}`;
    const end = `${this.year}-${this.month}-${monthLastDate}`;
    // this._service.loadVideo(this.plan.id, start, end).subscribe(url => this.src = url);
    this.src = this._service.getPlanVideo(this.plan.id, start, end);
  }

  close() {
    this.onModalClose.emit(true);
  }
}
