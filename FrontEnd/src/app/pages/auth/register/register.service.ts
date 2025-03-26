import { Injectable } from '@angular/core';
import { Signup } from '../../../models/signup';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthClient, UserTeleramDTO } from '../../../nswag/nswag.auth';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  constructor(
    private router:Router,
    private toastr:ToastrService,
    private authClient:AuthClient
  ) { }
  
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

  onLogin(user: UserTeleramDTO|any) {
    this.userOutput = JSON.stringify(user, null, 4);
    
    this.authClient.signUpWithTelegram(this.userOutput,user).subscribe({
      next:res=>{
        if(res==true){
          this.router.navigateByUrl('/profile')
        }

      },
      error:err=>{
        if(err.status==401){
          this.onLoginArea = false;
        }
      }
    });

    // this.httpService.LoginAsTelegram(this.userOutput).subscribe({
    //   next:resp=>{
    //     if(resp==true){
    //       this.router.navigateByUrl('/');
          
    //     }
    //   },
    //   error:(err:HttpErrorResponse)=>{
    //     if(err.status==401){
    //       this.onLoginArea = false;
    //     }
    //   }
    // })
  }

  visible: boolean = false;

  
  submitSignUp(data:Signup){

    this.authClient.signUpWithTelegram(this.userOutput,{
      userName:data.username,
      email:data.email,
      password:data.password,
     } as UserTeleramDTO).subscribe({
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
    
    // this.httpService.SignUpAsTelergam(data,this.userOutput).subscribe({
    //   next:res=>{
    //     if(!res){
    //       this.toastr.error("You have blocked the bot","Telegram bot");
    //       this.visible=true;
    //     }
    //     else{
    //       this.toastr.success("You entered!")
    //       this.router.navigateByUrl('/profile')
    //     }
    //   }
    // });
    
  }

 
}
