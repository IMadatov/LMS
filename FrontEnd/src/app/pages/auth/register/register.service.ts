import { Injectable } from '@angular/core';
import { TelegramUserData } from '@telegram-auth/server';
import { HttpService } from '../../../services/http.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Signup } from '../../../models/signup';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  constructor(private httpService:HttpService,private router:Router,private toastr:ToastrService) { }
  
  onLoginArea:boolean= true;
  isLoad=false;
  isLoadError=false;
  userOutput = '';

  botName = 'lms_manager_bot';

  onLoad() {
    this.isLoad = true;
  }

  onLoadError() {
    this.isLoadError = true;
  }

  onLogin(user: TelegramUserData|any) {
    this.userOutput = JSON.stringify(user, null, 4);
    
    this.httpService.LoginAsTelegram(this.userOutput).subscribe({
      next:resp=>{
        if(resp==true){
          this.router.navigateByUrl('/');
          
        }
      },
      error:(err:HttpErrorResponse)=>{
        if(err.status==401){
          this.onLoginArea = false;
        }
      }
    })
  }

  visible: boolean = false;

  
  submitSignUp(data:Signup){

    
    this.httpService.SignUpAsTelergam(data,this.userOutput).subscribe({
      next:res=>{
        if(!res){
          this.toastr.error("You have blocked the bot","Telegram bot");
          this.visible=true;
        }
        else{
          this.toastr.success("You entered!")
          this.router.navigateByUrl('/profile')
        }
      }
    });
    
  }

 
}
