import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { CommonModule } from '@angular/common';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { FormsModule } from '@angular/forms';
import { TranslationService } from '../../services/translation.service';
import { TranslocoModule } from '@jsverse/transloco';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    CascadeSelectModule,
    FormsModule,
    TranslocoModule
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
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
  public countries: any[] | undefined;

  public selectedLanguage: any;

  ngOnInit(): void {
    this.profileService.getUserData();
  }

  LanguageChange(){
    this.translationService.ChangeLanguageUser();
  }
}
