import { Component } from '@angular/core';

import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoadingService } from '../../services/loading.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [MatProgressSpinnerModule,
    CommonModule
  ],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.css'
})
export class SpinnerComponent {
  
  constructor(public loadingService:LoadingService) {
      
  }
}
