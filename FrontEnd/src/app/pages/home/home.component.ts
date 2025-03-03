import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { RouterOutlet } from '@angular/router';
import { SpinnerComponent } from "../spinner/spinner.component";
import { AuthService } from '../auth/auth.service';
import { TranslationService } from '../../services/translation.service';
import { TranslocoService } from '@jsverse/transloco';
import { Subscription, take } from 'rxjs';

@Component({
    selector: 'app-home',
    imports: [HeaderComponent, RouterOutlet],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {


  constructor(
    private authService: AuthService,
    private translationService: TranslationService
  ) {

  }


  ngOnInit() {
    this.authService.getCurrentUser();

    this.translationService.GetCurrentLanguage();

    // this.subscribtion= this.translocoService.load(this.translationService.currentLanguage?.code||"en").pipe(take(1)).subscribe(()=>{
    //   this.translocoService.setActiveLang(this.translationService.currentLanguage?.code || "en");
    // })
  }


}

/*

 this.subscribtion.unsubscribe();
    this.subscribtion = this.translocoService.load(this.translationService.currentLanguage?.code || "en").pipe(take(1)).subscribe(() => {
      this.translocoService.setActiveLang(this.translationService.currentLanguage?.code! || "en")
    })
*/