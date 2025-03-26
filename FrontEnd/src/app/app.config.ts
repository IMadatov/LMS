import {
  ApplicationConfig,
  provideZoneChangeDetection,
  isDevMode,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { loadingInterceptor } from './services/loading.interceptor';
import { provideToastr } from 'ngx-toastr';
import { TranslocoHttpLoader } from './transloco-loader';
import { provideTransloco, provideTranslocoMissingHandler } from '@jsverse/transloco';
import { TranslocoCustomMissingHandler } from './custom-missing-handler';
import { providePrimeNG } from 'primeng/config';
import primengTheme from './primengTheme';
import { API_BASE_URL } from './nswag/nswag.auth';
import { environment } from '../environments/environment';
import { jwtTokenInterceptor } from './interceptors/jwt-token.interceptor';
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimations(),
    provideAnimationsAsync(),
    provideZoneChangeDetection(),
    provideHttpClient(),
    provideHttpClient(withInterceptors([loadingInterceptor,jwtTokenInterceptor])),
    provideToastr(),
    provideHttpClient(),
    provideTransloco({
      config: {
        availableLangs: ['en','uz','ru','kr'],
        defaultLang: 'en',
        // Remove this option if your application doesn't support changing language in runtime.
        reRenderOnLangChange: true,
        prodMode: !isDevMode(),
        missingHandler:{
          allowEmpty:true
          
        }
      },
      loader: TranslocoHttpLoader,
    }),
    provideTranslocoMissingHandler(TranslocoCustomMissingHandler),
    providePrimeNG({
      theme:{
        preset:primengTheme,
        options:{
          darkModeSelector:'.dark'
        }
      }
    }),
    {
      provide: API_BASE_URL,
      useValue: environment.API_BASE_URL,
    },
    
 
  ],
};
