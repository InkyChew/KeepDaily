import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '../services/translate.service';
import { Category } from '../models/calendar';
import { Observable, map, of } from 'rxjs';

@Pipe({
  name: 'ctgname'
})
export class CategoryPipe implements PipeTransform {

  constructor(private _translateService: TranslateService) {}

  transform(ctg?: Category): Observable<string | null> {
    if(!ctg) return of(null);
    return this._translateService.curLang$.pipe(
      map(lang => {
        switch(lang) {
          case 'zh-TW':
            return ctg.name_zh;
          default:
            return ctg.name;
        }
      })
    );
  }

}
