import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { SideBarComponent } from './components/header/side-bar/side-bar.component';
import { PrimeNGConfig } from 'primeng/api';
import { SpinnerComponent } from "./pages/spinner/spinner.component";
import { TranslationService } from './services/translation.service';
import { ThemeService } from './services/theme.service';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    SpinnerComponent
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(
    private primeNgConfig: PrimeNGConfig,
    private themeService: ThemeService,
    ) {
    this.primeNgConfig.csp.set({ nonce: '...' });
  }

  ngOnInit(): void {
    this.primeNgConfig.ripple = true;
    this.primeNgConfig.zIndex = {
      modal: 1100, // dialog, sidebar
      overlay: 1000, // dropdown, overlaypanel
      menu: 1000, // overlay menus
      tooltip: 1100, // tooltip
    };
    this.themeService.loadTheme();
  }


  title = 'Application';
}
