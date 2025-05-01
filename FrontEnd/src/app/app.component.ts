import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SpinnerComponent } from "./pages/spinner/spinner.component";
import { ThemeService } from './services/theme.service';
import { AuthService } from './pages/auth/auth.service';
@Component({
    selector: 'app-root',
    imports: [
        RouterOutlet,
        SpinnerComponent
    ],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    standalone:true,
})
export class AppComponent implements OnInit {
  constructor(
    private themeService: ThemeService,
    private authService:AuthService
    ) {
 
  }

  ngOnInit(): void {
    this.authService.getCurrentUser();
    this.themeService.loadTheme();
  }


  title = 'LMS-Angular';
}
