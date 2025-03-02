import { Component } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { AccordionModule } from 'primeng/accordion';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tests',
  standalone: true,
  imports: [PanelModule,CommonModule,AccordionModule],
  templateUrl: './tests.component.html',
  styleUrl: './tests.component.css'
})
export class TestsComponent {
  tabs = [
    { title: 'Title 1', content: 'Content 1' },
    { title: 'Title 2', content: 'Content 2' },
    { title: 'Title 3', content: 'Content 3' }
];
}
