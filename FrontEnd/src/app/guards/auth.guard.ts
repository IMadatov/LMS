import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { combineLatest, map } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { AuthService } from '../pages/auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const loadingService = inject(LoadingService);
  const authService = inject(AuthService);

  return combineLatest([authService.authenticated$]).pipe(map((x) => {
    const authenticated = x[0];
    // console.log(authenticated);

    if (authenticated) {

      authService.getPageForRole();
      
      if(!authService.CheckNavigationIsForRole(route.url)){
        router.navigateByUrl("**")
      }

      return true;
    }

    loadingService.setLoading(true);
    router.navigateByUrl('/auth/register');
    authService.signOut();
    return false;
  }));
 

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
  const authService = inject(AuthService);
  const router = inject(Router);


  return combineLatest([authService.authenticated$]).pipe(
    map((x)=>{

      const authenticated = x[0];
      // console.log(authenticated);
      if (authenticated) {
        router.navigateByUrl('/');
        return false;
      }

      return true;
    })
  );


};

