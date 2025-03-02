import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { TranslocoService } from '@jsverse/transloco';
import { map, Subscription, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslationService {
  constructor(
    private httpService: HttpService,
    private translocoService: TranslocoService
  ) {
    this.SetCurrentLanguageAsActiveLangTransloco();
  }


  private subscribtion: Subscription = Subscription.EMPTY;
  
  public currentLanguage: Country | undefined;

  public countries: Country[] | any = [
    {
      name: "O'zbek",
      code: 'uz'
    },
    {
      name: 'Qaraqalpaq',
      code: 'kr'
    },
    {
      name: 'English',
      code: 'en'
    },
    {
      name: 'Русский',
      code: 'ru'
    }
  ];

  SetCurrentLanguageAsActiveLangTransloco(){
    this.currentLanguage=this.countries.filter((c:Country)=>{
      return c.code.toString()===this.translocoService.getActiveLang();
    })[0];
  }

  get CurrentLanguageOnSite(){    
    return this.countries.filter((c:Country)=>{
      return c.code ===  this.translocoService.getActiveLang()
    });
  }

  // BuildSubscribtion(){
  //   this.subscribtion.unsubscribe();
  //   this.subscribtion=this.translocoService.load(this.currentLanguage?.code|| "en").pipe(
  //     take(1)).subscribe(()=>{
  //     this.translocoService.setActiveLang(this.currentLanguage?.code||"en");
  //   })
  // }

  // DestorySubscribtion(){
  //   this.subscribtion.unsubscribe();
  // }

  GetCurrentLanguage() {
    this.httpService.UserLanguage().subscribe({
      next: (value) => {
       this.translocoService.setActiveLang(value.language) 
      this.SetCurrentLanguageAsActiveLangTransloco();
            
        // this.BuildSubscribtion();
      }

    });
  }

  ChangeLanguageUser(){
    this.httpService.ChangeLanguage(this.currentLanguage?.code||'en').subscribe((value)=>{
      this.translocoService.setActiveLang(value.language);
      
    });
  }

}

interface Country {
  name: string;
  code: string;
}