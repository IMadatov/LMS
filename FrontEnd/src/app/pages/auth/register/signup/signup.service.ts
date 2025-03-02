import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';
import { HttpService } from '../../../../services/http.service';

@Injectable({
  providedIn: 'root',
})
export class SignupService {

  checkUserName:boolean|undefined;

  constructor(private httpService:HttpService) {}

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
    
    
    this.httpService.CheckUserName(name).subscribe({
      next:resp=>{
        this.checkUserName=resp;
      }
    })
  }

  
}
