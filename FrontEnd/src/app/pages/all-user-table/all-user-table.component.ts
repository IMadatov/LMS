import { Component, OnDestroy, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { RatingModule } from 'primeng/rating';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { UserService } from './user.service';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { User } from '../../models/user';
import { DropdownModule } from 'primeng/dropdown';
import { PaginatorModule } from 'primeng/paginator';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { AvatarModule } from 'primeng/avatar';
import { ImageModule } from 'primeng/image';
import { Router } from '@angular/router';
import {RadioButtonModule} from 'primeng/radiobutton'
import { AuthService } from '../auth/auth.service';

@Component({
    selector: 'app-all-user-table',
    imports: [
        TableModule,
        TagModule,
        RatingModule,
        ButtonModule,
        CommonModule,
        MultiSelectModule,
        FormsModule,
        DialogModule,
        AvatarModule,
        CascadeSelectModule,
        DropdownModule,
        PaginatorModule,
        ImageModule,
        RadioButtonModule
    ],
    providers: [UserService],
    templateUrl: './all-user-table.component.html',
    styleUrl: './all-user-table.component.css'
})
export class AllUserTableComponent implements OnInit, OnDestroy {
  count: number = 1;

  visible: boolean = false;
  clearDropDownVisible: boolean = false;
  defaultAvatar = '/account_circle_24dp_E8EAED_FILL0_wght400_GRAD0_opsz24.svg';

  constructor(public userService: UserService, public router: Router,public authService:AuthService) {}

  ngOnInit() {
    this.userService.getUserData();
  }

  ngOnDestroy(): void {
    this.userService._unsubscribe.next();
    this.userService._unsubscribe.complete();
  }



  showDialog(user: User) {
    // this.userService.selectedRoles=user.roles?.at(0);
    this.userService.selectUser = user;
    this.visible = true;
    switch (user.active) {
      case true:
        this.userService.selectedAccountStatus = this.userService.categories[0];
        break;
      case false:
        this.userService.selectedAccountStatus = this.userService.categories[1];
        break;
    }

    switch(user.roles?.at(0)){
      case "admin": this.userService.selectedRole=this.userService.roles?.at(0);break;
      case "teacher":this.userService.selectedRole=this.userService.roles?.at(1);break;
      case "student":this.userService.selectedRole=this.userService.roles?.at(2);break;
    }
  }

}
