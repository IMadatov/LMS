import { Component } from '@angular/core';
import { LoadingService } from '../../services/loading.service';
import { CommonModule } from '@angular/common';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
@Component({
    selector: 'app-spinner',
    imports: [
        CommonModule,
        ProgressSpinnerModule
    ],
    templateUrl: './spinner.component.html',
    styleUrl: './spinner.component.css',
    standalone:true
})
export class SpinnerComponent {
  
  constructor(public loadingService:LoadingService) {
      
  }
}
