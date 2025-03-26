import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

export const jwtTokenInterceptor: HttpInterceptorFn = (req, next) => {

  const router=inject(Router);
  if(req.url.includes('auth/register')){
    return next(req);
  }

  req=req.clone({
    setHeaders:{
      Authorization:"Bearer "+sessionStorage.getItem("accessToken")}
  })

  return next(req).pipe(tap((res)=>{
    if(res instanceof HttpErrorResponse){
      if(res.status==401){
        router.navigate(["auth/register"]);
      }
    }
   },
   )
);
  
};
