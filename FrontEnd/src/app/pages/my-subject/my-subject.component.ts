import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { MySubjectService } from './my-subject.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { AuthService } from '../auth/auth.service';
@Component({
  selector: 'app-my-subject',
  standalone: true,
  imports: [
    PanelModule,
    ButtonModule,
    TableModule,
    DialogModule,
    ReactiveFormsModule,
    PaginatorModule,
    CommonModule,
  ],
  templateUrl: './my-subject.component.html',
  styleUrl: './my-subject.component.css',
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class MySubjectComponent  {
  constructor(
    public mysubjectService: MySubjectService,
    private changeDetection:ChangeDetectorRef,
    public authService:AuthService
  ) {
    mysubjectService.changeDetector=changeDetection;
  }

  visible: boolean = false;

 

  showDialog() {
    this.visible = true;
    
    
  }
}
