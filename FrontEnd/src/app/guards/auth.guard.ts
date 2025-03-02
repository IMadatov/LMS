import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { HttpService } from '../services/http.service';
import { catchError, map, of, throwError } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { AuthService } from '../pages/auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const httpService = inject(HttpService);
  const router = inject(Router);
  const loadingService=inject(LoadingService);
  const authService=inject(AuthService);


  return httpService.OnSite().pipe(
    catchError((err)=>{
      if(err.status==0)router.navigateByUrl('server-error')
      return throwError(()=>{
        
      })
    }),
    map((x: string) => {
      loadingService.setLoading(true);
      if (x=="false" ) {
        router.navigateByUrl('auth/register');    
      }
      
      authService.roles=x;

      authService.getPageForRole();

      authService.accessUrlList= authService.getUrlsForRole(authService.navigationPages!);
      
      
      if(!authService.CheckNavigationIsForRole(route.url)){
        router.navigateByUrl("**")
      }
      
      return true;
    }),
    
  );
};

export const noAuthGuard: CanActivateFn = (route, state) => {
  const httpService = inject(HttpService);
  const router = inject(Router);
  return httpService.OnSite().pipe(
    map((x: string) => {
      if (x !="false") {
        router.navigateByUrl('/');
        return false;
      }
      return true;
    })
  );
};
