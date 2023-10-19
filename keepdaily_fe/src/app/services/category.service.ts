import { Injectable } from '@angular/core';
import { EnvService } from './env.service';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/calendar';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private _http: HttpClient, private _env: EnvService) { }

  getAllCategory() {
    return this._http.get<Array<Category>>(this._env.APIOption.CategoryEndpoint);
  }

  postCategory(ctg: Category) {
    return this._http.post<Category>(this._env.APIOption.CategoryEndpoint, ctg);
  }

  putCategory(ctg: Category) {
    return this._http.put<Category>(this._env.APIOption.CategoryEndpoint, ctg);
  }

  deleteCategory(id: number) {
    return this._http.delete(`${this._env.APIOption.CategoryEndpoint}/${id}`);
  }
}
