import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SideBarComponent } from './side-bar/side-bar.component';
import { HeaderService } from './header.service';
import { MenuModule } from 'primeng/menu';
import { CommonModule } from '@angular/common';
import { SignalrService } from '../../services/signalr.service';
import { TranslocoDirective, TranslocoModule } from '@jsverse/transloco';
import { ThemeService } from '../../services/theme.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    SideBarComponent,
    MenuModule,
    CommonModule,
    TranslocoModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
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
