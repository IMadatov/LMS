import { Component } from '@angular/core';
import { DividerModule } from 'primeng/divider';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TelegramLoginWidgetComponent } from './telegram-login-widget/telegram-login-widget.component';
import { AuthDataValidator } from '@telegram-auth/server';
import { TelegramUserData, urlStrToAuthDataMap } from '@telegram-auth/server/utils';
import { FloatLabelModule } from 'primeng/floatlabel';
import { LoginComponent } from "./login/login.component";
import { CommonModule, NgIf } from '@angular/common';
import { SignupComponent } from "./signup/signup.component";
import { RegisterService } from './register.service';

@Component({
  standalone: true,
  selector: 'app-register',
  styleUrl:'./register.component.css',
  imports: [
    DividerModule,
    ButtonModule,
    InputTextModule,
    TelegramLoginWidgetComponent,
    FloatLabelModule,
    LoginComponent,
    CommonModule,
    SignupComponent,
    NgIf
],

  templateUrl:"./register.component.html",
})
export class RegisterComponent {

  isVisibleLogin = true;
  constructor(public registerService:RegisterService) {}

 
}
