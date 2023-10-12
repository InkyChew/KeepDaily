import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '../services/translate.service';
import { Observable, map } from 'rxjs';

@Pipe({
  name: 'txs'
})
export class TranslatePipe implements PipeTransform {

  constructor(private _service: TranslateService) {}

  transform(key: string): Observable<string> {
    return this._service.curLang$.pipe(
      map(lang => {
        return this._service.translate(lang, key) ?? key;
      })
    );
  }
}
