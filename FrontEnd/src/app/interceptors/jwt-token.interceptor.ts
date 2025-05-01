import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, tap, throwError } from 'rxjs';
import { AuthService } from '../pages/auth/auth.service';

export const jwtTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  req = req.clone({
    setHeaders: {
      Authorization: "Bearer " + sessionStorage.getItem("accessToken")
    }
  })

  return next(req).pipe(
    
    catchError((err:HttpErrorResponse)=>{
      
      if(err.status==401){
        router.navigateByUrl("/auth/register")
        authService.signOut();
      }        
      return throwError(()=>err);
    })
    )


};
