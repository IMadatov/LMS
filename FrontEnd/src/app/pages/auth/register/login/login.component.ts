import { Component } from '@angular/core';
import { FloatLabelModule } from 'primeng/floatlabel';
import { RegisterService } from '../register.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth.service';
import { SignInDto } from '../../../../nswag/nswag.auth';

export const reg = RegExp(
  /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/
);

@Component({
  selector: 'app-login',
  imports: [FloatLabelModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true
})
export class LoginComponent {
  public isPasswordShow = false;

  constructor(
    public registerService: RegisterService,
    private authService:AuthService
  ) { }

  public userLogin = new FormGroup({
    userName: new FormControl('', [
      Validators.minLength(5),
      Validators.maxLength(25),
      Validators.pattern('^[A-Za-z]+[A-Za-z0-9]*$'),
      Validators.required,
    ]),
    password: new FormControl('', [
      Validators.minLength(7),
      Validators.maxLength(40),
      Validators.pattern(reg),
    ]),
    rememberme: new FormControl(''),
  });

  submitData() {
   this.authService.signIn({
    password:this.userLogin.controls.password.value?.toString(),
    username:this.userLogin.controls.userName.value?.toString(),
    rememberMe:this.userLogin.controls.rememberme.value=='true'?true:false
   } as SignInDto).subscribe(
    {
      next:(resp)=>{
        if(resp){
          location.reload();
          console.log('login success');
        }
      }
    }
   );
  }
}
