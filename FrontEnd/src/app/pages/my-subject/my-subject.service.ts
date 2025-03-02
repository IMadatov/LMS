import { ChangeDetectorRef, Injectable } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { ToastrService } from 'ngx-toastr';
import { Subject } from '../../models/subject';
import { FormControl } from '@angular/forms';
import { PaginatorState } from 'primeng/paginator';
import { SortEvent } from 'primeng/api';
import { User } from '../../models/user';
@Injectable({
  providedIn: 'root',
})
export class MySubjectService {

  countMySubjectItems = 0;

  first = 0;

  row = 10;

  field: string = 'Name';

  order: number = 1;

  selecteds: any[] = [];

  subjectName = new FormControl<string>('');

  listSubjects: Subject[] = [];

  totalItems: number | undefined;

  changeDetector?: ChangeDetectorRef | null;
  constructor(
    private httpService: HttpService,
    private toastr: ToastrService
  ) { }

  onPageChange($event: PaginatorState) {
    this.first = $event.first!;
    this.row = $event.rows!;
    this.GetSubjects();
  }

  CreateSubject(name: string) {
    this.httpService.CreateSubject({ name: name }).subscribe({
      next: (res) => {
        this.GetSubjects();
      },
    });
    this.subjectName.reset();
  }
  DeleteSubject() {
    const remaining = this.listSubjects.filter(
      (x) => !this.selecteds.includes(x)
    );
    this.listSubjects = remaining;
    this.selecteds.forEach((x) => (x.user = null));
    if (this.selecteds.length != 0) {
      this.httpService.DeleteSubject(this.selecteds as Subject[]).subscribe({
        next: (resp) => {
          // console.log(resp);

          if (resp == true) this.toastr.info('Deleteted subjects');
          else this.toastr.warning('There are no your subjects');
          this.selecteds = [];
          this.GetSubjects();
        },
        error: (err) => {
          console.error(err);
        },
      });
    }

  }
  GetSubjects() {
    let sortField = this.field;
    if (sortField == 'User') {
      sortField = 'UserId'
    }

    this.httpService
      .GetSubjects({ first: this.first, rows: this.row, sortField: sortField, sortOrder: this.order })
      .subscribe({
        next: (resp) => {
          this.countMySubjectItems = 0;
          this.listSubjects = resp.items as Subject[];
          this.totalItems = resp.totalItems;
          
          this.IncrementList();
          this.changeDetector?.markForCheck();
        },
        error: (err) => console.error(err),
      });
  }
  IncrementList() {
    let counter = 0;
    this.listSubjects?.forEach((x) => {
      x.indexOfItem = ++counter;
    });
  }
  SortFunction($event: any) {
    this.field = $event.sortField!;
    this.order = $event.sortOrder!;
    this.GetSubjects();
  }

  prefixCountMySubjectItems() {
    this.countMySubjectItems++;
  }
}
