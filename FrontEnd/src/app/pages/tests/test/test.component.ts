import { Component } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [CommonModule,TabViewModule],
  templateUrl: './test.component.html',
  styleUrl: './test.component.css'
})
export class TestComponent {
  activeIndex: number = 0;

  scrollableTabs: any[] = Array.from({ length: 50 }, (_, i) => ({ title: "Title", content: "Content" }));
}
