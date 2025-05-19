import { Injectable } from '@angular/core';
import { TranslocoService } from '@jsverse/transloco';
import { Languages, UserClient } from '../nswag/nswag.auth';

@Injectable({
  providedIn: 'root'
})
export class TranslationService {
  constructor(
    private userClient:UserClient,
    private translocoService: TranslocoService
  ) {
    this.SetCurrentLanguageAsActiveLangTransloco();
  }


  
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
    
    this.userClient.myLanguage().subscribe({
      next:(value)=>{
        this.translocoService.setActiveLang(value.language||"en");
        this.SetCurrentLanguageAsActiveLangTransloco();
      }
    });
  }

  ChangeLanguageUser(){

    this.userClient.changeMyLanguage(Languages.ENGLISH).subscribe({
      next:(value)=>{
        this.translocoService.setActiveLang(value.language||"en");
      }});
    
  }

}

interface Country {
  name: string;
  code: string;
}