import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {

  private _storageKey = 'lang';
  defaultLang = localStorage.getItem(this._storageKey) ?? 'en-US';
  curLang$ = new BehaviorSubject<string>(this.defaultLang);
  private _dataset: any = {};

  constructor(private _http: HttpClient) {}
  
  loadData() {
    return new Observable(observer => {
      this._http.get('../../assets/translate.json').subscribe(res => {
        this._dataset = res;
        observer.next(res);
        observer.complete();
      });
    }); 
  }

  setLang(lang: string) {
    this.curLang$.next(lang);
    localStorage.setItem(this._storageKey, lang);
  }

  translate(lang: string, key: string): string {
    key = `${key.charAt(0).toUpperCase()}${key.slice(1)}`;
    return this._dataset[lang][key];
  }
}
