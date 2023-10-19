import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/calendar';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../services/form.service';

@Component({
  selector: 'app-setting-category',
  templateUrl: './setting-category.component.html',
  styleUrls: ['./setting-category.component.css']
})
export class SettingCategoryComponent implements OnInit {

  ctgList: Category[] = [];

  constructor(private _service: CategoryService) {}

  ngOnInit(): void {
    this.getCategoryList();
  }

  getCategoryList() {
    this._service.getAllCategory().subscribe(res => this.ctgList = res);
  }

  add() {
    this.ctgList.unshift(new Category());
  }

  save(ctg: Category, i: number) {
    const method = ctg.id == 0
            ? this._service.postCategory(ctg)
            : this._service.putCategory(ctg);
    method.subscribe(res => {
      this.ctgList[i] = res;
    });
  }

  delete(id: number, i: number) {
    if(id == 0) this.ctgList.shift();
    else if(confirm("Sure to delete?"))
      this._service.deleteCategory(id).subscribe(() => this.ctgList.splice(i,1));
  }
}
