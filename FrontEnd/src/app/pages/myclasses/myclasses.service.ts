import { ChangeDetectorRef, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TableLazyLoadEvent } from 'primeng/table';
import { Class } from '../../models/class';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MyclassesService {

  countMyClassitems = 0;

  first = 0;
  rows = 5;
  field: string = 'Name';
  order = 1;
  totalItems = 0;
  classesList: Class[] = [];

  selectionClass: Class[] = [];
  visible = false;
  cd?: ChangeDetectorRef | null;

  constructor(
     private toastr: ToastrService,
    private router:Router) { }

  SortFunc($event: TableLazyLoadEvent) {
    this.first = $event.first!;
    this.rows = $event.rows!;
    this.field = $event.sortField?.toString()!;
    this.order = $event.sortOrder!;

    
    this.GetClasses();

  }

  CreateClass(name: string, dagree: string) {
    // debugger
    // this.httpService.CreateClass(name, parseInt(dagree)).subscribe({
    //   next: resp => {
    //     if (resp)
    //       this.toastr.success("Class created");
    //     this.GetClasses();
    //   },
    //   error: err => console.error(err)

    // });
  }

  GetClasses() {
    // this.httpService.GetClasses(  {first: this.first,rows: this.rows,sortField: this.field,sortOrder: this.order}).subscribe({
    //   next: resp => {
    //     this.countMyClassitems = 0;
    //     this.classesList = resp.items as Class[] || [];
    //     this.totalItems = resp.totalItems || 0;
        
    //     let index = 1;
    //     this.classesList.forEach(x => {
    //       x.index = index++;
    //     });
    //     this.cd?.markForCheck();
    //   }
    // })
  }

  DeleteClasses() {
    // console.log(this.selectionClass);

    // this.httpService.DeleteClasses(this.selectionClass.map(x => x.id)).subscribe({
    //   next: resp => {
    //     this.toastr.info("Deleted items");
    //     this.selectionClass = [];
    //     this.GetClasses();
    //   }
    // })
  }

  prefixCountMyClassItems() {
    this.countMyClassitems++;
  }

  disableTrashButton(): boolean {
    return this.selectionClass.length == 0;
  }

  NavigateToClass(id:number){
    this.router.navigateByUrl("teacher/classes/class/"+id);
  }

}
