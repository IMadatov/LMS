import { Component } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
@Component({
    selector: 'app-students',
    imports: [TableModule, CommonModule, PanelModule, ButtonModule],
    templateUrl: './students.component.html',
    styleUrl: './students.component.css'
})
export class StudentsComponent {
  customers!: any[];

  constructor() {}

  ngOnInit() {
     
  }
}
