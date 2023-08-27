import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { ICategory } from '../models/calendar';

@Component({
  selector: 'app-setting-category',
  templateUrl: './setting-category.component.html',
  styleUrls: ['./setting-category.component.css']
})
export class SettingCategoryComponent implements OnInit {

  ctgList: ICategory[] = [];

  constructor(private _service: CategoryService) { }

  ngOnInit(): void {
    this.getCategoryList();
  }

  getCategoryList() {
    this._service.getAllCategory().subscribe(res => this.ctgList = res);
  }

  create(e: HTMLInputElement) {
    const name = e.value;
    const ctg: ICategory = {id:0, name: name};
    this._service.postCategory(ctg).subscribe(res => {
      this.ctgList.push(res);
      e.value = '';
    });
  }

  edit(ctg: ICategory, i: number) {
    this._service.putCategory(ctg).subscribe(res => this.ctgList[i] = res);
  }

  delete(id: number, i: number) {
    this._service.deleteCategory(id).subscribe(() => this.ctgList.splice(i,1));
  }

  closeEditor() {

  }
}
