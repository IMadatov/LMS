import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';
import { AuthClient } from '../../../../nswag/nswag.auth';

@Injectable({
  providedIn: 'root',
})
export class SignupService {

  checkUserName:boolean|undefined;

  constructor(
    private authClient:AuthClient
  ) {}

  patternValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (!control.value) return null;

      const regex = new RegExp('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$');

      const valid = regex.test(control.value);

      return valid ? null : { invalidPassword: true };
    };
  }

  MatchPassword(control:AbstractControl):any{
    const password:string =control.get('password')?.value;

    const confirmPassword:string = control.get("confirmPassword")?.value;

    if(!confirmPassword.length)
      return null;

    if(confirmPassword.length<8)
      control.get("confirmPassword")?.setErrors({minLength:true});
    else {
      if(password!=confirmPassword)
        control.get('confirmPassword')?.setErrors({misMatch:true});
      else return null;
    }
  }

  CheckUserName(name:string){
    
    this.authClient.checkUsername(name).subscribe({
      next:resp=>{
        this.checkUserName=resp;
      }});
  }

  
}
