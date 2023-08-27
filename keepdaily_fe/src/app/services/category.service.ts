import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { HttpClient } from '@angular/common/http';
import { ICategory } from '../models/calendar';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAllCategory() {
    return this._http.get<Array<ICategory>>(this._env.APIOption.CategoryEndpoint);
  }

  postCategory(ctg: ICategory) {
    return this._http.post<ICategory>(this._env.APIOption.CategoryEndpoint, ctg);
  }

  putCategory(ctg: ICategory) {
    return this._http.put<ICategory>(this._env.APIOption.CategoryEndpoint, ctg);
  }

  deleteCategory(id: number) {
    return this._http.delete(`${this._env.APIOption.CategoryEndpoint}/${id}`);
  }
}
