import { Component, OnInit } from '@angular/core';
import { Plan } from '../models/calendar';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {

  planList: Plan[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
