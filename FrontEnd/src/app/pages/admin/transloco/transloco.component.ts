import { CommonModule } from '@angular/common';
import { AfterViewChecked, Component, ElementRef, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import{InputIconModule} from 'primeng/inputicon'
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TranslocoTableService } from './transloco-table.service';
import{IconFieldModule} from 'primeng/iconfield'
import { debounceTime, distinctUntilChanged } from 'rxjs';
@Component({
  selector: 'app-transloco',
  standalone: true,
  imports: [
    TableModule,
    CommonModule, 
    ToastrModule, 
    TagModule, 
    DropdownModule, 
    ButtonModule, 
    InputTextModule, 
    FormsModule,
    InputIconModule,
    IconFieldModule
  ],
  templateUrl: './transloco.component.html',
  styleUrls: ['./transloco.component.scss'],
})
export class TranslocoComponent implements AfterViewChecked ,OnInit,OnDestroy{
  @ViewChild('enterClickSaveBtn') saveBtn: ElementRef<HTMLButtonElement> | undefined;

  elementOnChangeSave: HTMLElement | undefined | null;

  constructor(
    public translocoTableService: TranslocoTableService
  ) {
    
  }
  
  ngOnDestroy(): void {
    this.translocoTableService.searchSubject.unsubscribe();
  }

  ngOnInit(): void {
    this.translocoTableService.searchSubject.pipe(
      debounceTime(800),
    distinctUntilChanged()
  ).subscribe((globalFilter)=>{
    this.translocoTableService.globalFilter=globalFilter;
    this.translocoTableService.GetTranslocoItems();
  })
  }

  ngAfterViewChecked(): void {
    if (this.saveBtn) {
      console.log(this.saveBtn.nativeElement);
    }
  }

  @HostListener('window:keydown.enter', ['$event'])
  ClickEnter(event: any) {
    console.log(this.elementOnChangeSave);
    
    if (this.elementOnChangeSave) {
      // this.elementOnChangeSave?.click();
  
      
    }
    
  }

  GlobalFilter(event:any | HTMLInputElement){
    this.translocoTableService.searchSubject.next((event.target as HTMLInputElement).value);
  }
}
