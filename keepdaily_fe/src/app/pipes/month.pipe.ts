import { Pipe, PipeTransform } from '@angular/core';
import { HelperService } from '../services/helper.service';

@Pipe({
  name: 'month'
})
export class MonthPipe implements PipeTransform {

  constructor(private _helper: HelperService) {}

  transform(value: number, ...args: unknown[]): unknown {
    return this._helper.Month[value];
  }

}
