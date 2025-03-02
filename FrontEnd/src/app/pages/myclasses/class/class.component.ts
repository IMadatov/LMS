import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabMenuModule } from 'primeng/tabmenu';
import { StudentsComponent } from "./students/students.component";
import { SubjectsComponent } from "./subjects/subjects.component";
import { TestsComponent } from "./tests/tests.component";
import { MessagesComponent } from "./messages/messages.component";
import { PanelModule } from 'primeng/panel';
import { ClassService } from './class.service';
@Component({
  selector: 'app-class',
  standalone: true,
  imports: [
    TabMenuModule,
    CommonModule,
    StudentsComponent, 
    SubjectsComponent, 
    TestsComponent, 
    MessagesComponent,
    PanelModule
  ],
  templateUrl: './class.component.html',
  styleUrl: './class.component.css'
})
export class ClassComponent {

  subjectId:number|undefined;

  constructor(
    private activeRouted:ActivatedRoute,
    public classService:ClassService
  ) {
    
  }
  
  items: any[] | undefined;
  
  activeItem:any|undefined;
  
  ngOnInit() {
    this.subjectId=Number(this.activeRouted.snapshot.params["id"]);
    this.items = [
      { label: 'Students', icon: 'pi pi-users' },
      { label: 'Subjects', icon: 'pi pi-book' },
      { label: 'Tests', icon: 'pi pi-list-check' },
      { label: 'Messages', icon: 'pi pi-inbox' }
    ]
    this.activeItem = this.items[0];
  }
  

  onActiveItemChange(event: any) {
    this.activeItem = event;
  }
}
