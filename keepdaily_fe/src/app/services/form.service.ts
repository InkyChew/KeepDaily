import { Injectable } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  constructor(private _group: FormGroup) { }

  passwordMatchValidation(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {      
      const pwd = control.get('password');
      const cpwd = control.get('confirmpassword');

      return (pwd?.dirty && cpwd?.dirty && pwd.value != cpwd.value)
      ? {passwordMismatch: true} : null;
    }
  }

  getErrorMsg(field: string) {
    let msg = '';
    if(field) {
      const control: FormControl = (this._group.controls as any)[field];
      if(control.dirty) {
        if(control.hasError('required'))
          msg = `${field.charAt(0).toUpperCase()}${field.slice(1)} is required`;
        else if((field == 'password' || field == 'confirmpassword') && this._group.hasError('passwordMismatch'))
          msg = `Password and Confirm Password should be the same`;
      }
    }
    return msg;
  }
}
