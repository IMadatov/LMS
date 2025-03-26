import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SpinnerComponent } from "./pages/spinner/spinner.component";
import { ThemeService } from './services/theme.service';
import { TranslocoCustomMissingHandler } from './custom-missing-handler';
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
    ) {
 
  }

  ngOnInit(): void {
    this.themeService.loadTheme();
  }


  title = 'Application';
}
