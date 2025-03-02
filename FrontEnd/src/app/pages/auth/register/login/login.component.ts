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
import { Login } from '../../../../models/login';
import { LoginService } from './login.service';

export const reg = RegExp(
  /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/
);

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FloatLabelModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  public isPasswordShow = false;

  constructor(
    public registerService: RegisterService,
    private loginService: LoginService
  ) {}

  public userLogin = new FormGroup({
    userName: new FormControl('', [
      Validators.minLength(7),
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
    this.loginService.login({
      username :this.userLogin.value.userName,
      password : this.userLogin.value.password,
      rememberMe : 'true'===this.userLogin.value.rememberme,
    });
  }
}
