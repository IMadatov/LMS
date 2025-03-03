import { Component, OnInit } from '@angular/core';

import { SideBarComponent } from './side-bar/side-bar.component';
import { HeaderService } from './header.service';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';
import { SignalrService } from '../../services/signalr.service';
import { TranslocoModule } from '@jsverse/transloco';
import { ThemeService } from '../../services/theme.service';
import { ToolbarModule } from 'primeng/toolbar';
@Component({
    selector: 'app-header',
    imports: [
        SideBarComponent,
        MenuModule,
        CommonModule,
        TranslocoModule,
        ToolbarModule
    ],
    templateUrl: './header.component.html',
    styleUrl: './header.component.css',
    standalone:true
})
export class HeaderComponent implements OnInit {
  
  isDarkMode=false;

  constructor(
    public headerService:HeaderService,
    public signalRService:SignalrService,
    public themeService:ThemeService
  ) {
  }
  ngOnInit(): void {
    this.isDarkMode=this.themeService.isDarkMode;
  }

}
