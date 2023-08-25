import { Component, OnInit } from '@angular/core';
import { IUser } from '../models/user';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  user!: IUser;
  constructor() { }

  ngOnInit(): void {
  }

}
