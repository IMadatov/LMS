import { Injectable } from '@angular/core';
import { Login } from '../../../../models/login';
import { HttpService } from '../../../../services/http.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private httpService: HttpService,private router:Router,private toastr:ToastrService) {}

  login(loginData: Login) {
    this.httpService.Login(loginData).subscribe({
      next: (resp) => {
        this.router.navigateByUrl('/');
      },
      error:(err:HttpErrorResponse)=>{
        if(err.status==400){
          this.toastr.error("User name or password incorrect");
        }
      }
    });
  }
}
