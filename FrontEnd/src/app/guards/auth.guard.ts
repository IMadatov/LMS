import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { catchError, map, of, throwError } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { AuthService } from '../pages/auth/auth.service';
import { AuthClient } from '../nswag/nswag.auth';
import { HttpErrorResponse } from '@angular/common/http';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const loadingService = inject(LoadingService);
  const authService = inject(AuthService);
  const authClient = inject(AuthClient);

  return authClient.onSite().pipe(
    map((x: string) => {
        
      loadingService.setLoading(true);
      if (x == "false") router.navigateByUrl('auth/register');
      authService.roles = x;
      authService.getPageForRole();
      authService.accessUrlList = authService.getUrlsForRole(authService.navigationPages!);
      if (!authService.CheckNavigationIsForRole(route.url)) {
        router.navigateByUrl("**")
      }
      return true;
    }),
    catchError((err: HttpErrorResponse) => {
      if (err.status == 0) router.navigateByUrl('server-error')
      return throwError(() => { })
    })
  )

  // return httpService.OnSite().pipe(
  //   catchError((err)=>{
  //     if(err.status==0)router.navigateByUrl('server-error')
  //     return throwError(()=>{

  //     })
  //   }),
  //   map((x: string) => {
  //     loadingService.setLoading(true);
  //     if (x=="false" ) {
  //       router.navigateByUrl('auth/register');    
  //     }

  //     authService.roles=x;

  //     authService.getPageForRole();

  //     authService.accessUrlList= authService.getUrlsForRole(authService.navigationPages!);


  //     if(!authService.CheckNavigationIsForRole(route.url)){
  //       router.navigateByUrl("**")
  //     }

  //     return true;
  //   }),

  // );
};
//Qaytaldan ko'rib chiguw karak
export const noAuthGuard: CanActivateFn = (route, state) => {
  const authClient = inject(AuthClient);
  const router = inject(Router);
  return authClient.onSite().pipe(
    map((x: string) => {
      if (x != "false") {
        router.navigateByUrl('/');
        return false;
      }
      return true;
    })
  );
};
