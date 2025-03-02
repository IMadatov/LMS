import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { User } from '../../models/user';
import { ToastrService } from 'ngx-toastr';
import { SelectorButtonModule } from '../../models/selector-button-module';
import { PaginatedList } from '../../models/paginated-list';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class UserService {

  public isAdmin:boolean=false;

  public users: User[] | undefined;

  public paginationList: PaginatedList | undefined;

  public roles?: SelectorButtonModule[];

  public selectedRole: SelectorButtonModule | undefined;

  public selectUser: User | undefined;

  public photoisValid: boolean = false;

  public image = new Image();

  imageSize: { width: number; height: number } | null = null;

  counter = 0;

  _unsubscribe: Subject<void> = new Subject();

  constructor(
    private httpService: HttpService,
    private authService:AuthService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

 

  getUserData(): void {
    this.route.url.pipe(takeUntil(this._unsubscribe)).subscribe({
      next: () => {
        var activeUrl = this.router.url;

        switch (activeUrl) {
          case '/admin/allusers':
            this.getUsers();
            break;
          case '/admin/allusers/admins':
            this.getAdmins();
            break;
          case '/admin/allusers/teachers':
            this.getTeachers();
            break;
          case '/admin/allusers/students':
            this.getStudents();
            break;
        }
      },
    });
  }

  getUsers() {
    this.httpService.GetAllUsers({first:this.first,rows:this.rows}).subscribe({
      next: (resp) => {
        this.paginationList = resp;
        this.users = this.paginationList.items;
        this.incrementCount();
      },
    });
  }

  getTeachers() {
    this.httpService.GetTeachers({rows:this.rows,first:this.first}).subscribe({
      next: (resp) => {
        this.paginationList = resp;

        this.users = this.paginationList.items;
        this.incrementCount();
      },
      error: (err) => console.error(err),
    });
  }
  getStudents() {
    this.httpService.GetStudents({first:this.first,rows:this.rows,sortField:'id',sortOrder:1}).subscribe({
      next: (resp) => {
        this.paginationList = resp;
        this.users = this.paginationList.items;
        this.incrementCount();
      },
      error: (err) => console.error(err),
    });
  }
  getAdmins() {
    this.httpService.GetAdmins({first:this.first,rows:this.rows,sortField:'id',sortOrder:1}).subscribe({
      next: (resp) => {
        this.paginationList = resp;
        this.users = this.paginationList.items;
        this.incrementCount();
      },
      error: (err) => console.error(err),
    });
  }

  incrementCount() {
    this.counter = 0;
    this.users?.forEach((x) => {
      x.countNumber = ++this.counter;
    });
    this.roles = [
      { name: 'Admin', code: 'admin' },
      { name: 'Teacher', code: 'teacher' },
      { name: 'Student', code: 'student' },
    ];
  }

  updataRole() {
    
    this.httpService
      .UpdataRoleUser({
        userId: this.selectUser?.id,
        role: this.selectedRole?.code,
        accountStatus:this.selectedAccountStatus.key=='true'?true:false
      })
      .subscribe({
        next: (resp) => {
          if (resp) this.toastr.success('Role change', 'successfully');
          else this.toastr.success('Role change', 'unsuccessful');
          this.getUserData();
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  getFIO() {
    return this.selectUser?.firstName + ' ' + this.selectUser?.lastName;
  }

  first: number = 0;
  rows: number = 10;
  onPageChange(event: PageEvent | any) {
    this.first = event.first;
    this.rows = event.rows;
    this.getUserData();
  }

  
  selectedAccountStatus: any = null;

  categories: any[] = [
    { name: 'Activate', key: 'true' },
    { name: 'Deactivate', key: 'false' },
  ];

}
interface PageEvent {
  first: number | undefined;
  rows: number | undefined;
  page: number | undefined;
  pageCount: number | undefined;
}
