import { Component } from '@angular/core';
import { RegisterService } from '../register.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SignupService } from './signup.service';
import { FuseValidators } from '../validators';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';

export const reg = RegExp(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/
);

@Component({
    selector: 'app-signup',
    imports: [ReactiveFormsModule, CommonModule, DialogModule, ButtonModule],
    templateUrl: './signup.component.html',
    styleUrl: './signup.component.css'
})
export class SignupComponent {
 

  public isPasswordShow=false;


  constructor(public registerService: RegisterService,public signUpService:SignupService) {}

  public userSignUp = new FormGroup(
    {
      userName: new FormControl('', [
        Validators.minLength(7),
        Validators.maxLength(25),
        Validators.pattern('^[A-Za-z]+[A-Za-z0-9]*$'),
        Validators.required,
      ]),
      email: new FormControl('', [Validators.email]),
      password: new FormControl(
        '',
        [
          Validators.minLength(7),
          Validators.maxLength(40),
          Validators.pattern(reg)
        ]
      ),
      confirmPassword: new FormControl(''),
      
    },
    {
      validators:[FuseValidators.mustMatch('password', 'confirmPassword')]
    }
  );

  checkUserName(name:string){
    if(this.userSignUp.controls.userName.valid)
      this.signUpService.CheckUserName(name);
    else this.signUpService.checkUserName=false;
  }

  submitData(){
    this.registerService.submitSignUp({
      username:this.userSignUp.value.userName,
      password:this.userSignUp.value.password,
      email:this.userSignUp.value.email,
      rememberme:true
    })
  }

  visible: boolean = false;

  showDialog() {
      this.visible = true;
  }
}
