import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { CommonModule } from '@angular/common';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { FormsModule } from '@angular/forms';
import { TranslationService } from '../../services/translation.service';
import { TranslocoModule } from '@jsverse/transloco';
import { SelectModule } from 'primeng/select';

@Component({
    selector: 'app-profile',
    imports: [
        CommonModule,
        CascadeSelectModule,
        FormsModule,
        TranslocoModule,
        SelectModule
    ],
    templateUrl: './profile.component.html',
    styleUrl: './profile.component.css',
    standalone:true
})
export class ProfileComponent implements OnInit{

  /**
   *
   */
  defaultAvatar = '/account_circle_24dp_E8EAED_FILL0_wght400_GRAD0_opsz24.svg';

  constructor(
    public profileService:ProfileService,
    public translationService:TranslationService
  ) {
    
  } 
  ngOnInit(): void {
    this.profileService.getUserData();
  }

  LanguageChange(){
    this.translationService.ChangeLanguageUser();    
  }
}
