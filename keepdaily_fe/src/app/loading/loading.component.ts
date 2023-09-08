import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HelperService } from '../services/helper.service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {

  loading$: Observable<boolean> = this._helperService.loader$;

  constructor(private _helperService: HelperService) { }

  ngOnInit(): void { }

}
