import { Injectable } from '@angular/core';
import { NavigationItem } from '../../models/navigation-item';
import { defaultNavigation } from './navigations';
import { Roles } from '../../models/roles';
import { User } from '../../models/user';
import { HttpService } from '../../services/http.service';
import { SignalrService } from '../../services/signalr.service';
import { UrlSegment } from '@angular/router';
import { MySubjectService } from '../my-subject/my-subject.service';
import { TranslationService } from '../../services/translation.service';
import { TranslocoService } from '@jsverse/transloco';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  currentUser: User | undefined;
  roles: string | undefined;
  navigationPages: NavigationItem[] | undefined;

  accessUrlList: string[] = [];
  constructor(
    private httpService: HttpService,
    private signalRService: SignalrService,
    private mysubjectService:MySubjectService

  ) { }

  getRoles(): any {
    switch (this.roles) {
      case 'admin':
        return Roles.Admin;
      case 'teacher':
        return Roles.Teacher;
      case 'student':
        return Roles.Student;
    }
  }

  getCurrentUser() {
    this.signalRService.connect();

    this.httpService.AboutMe().subscribe({
      next: (resp) => {
        this.currentUser = resp;
        
        this.mysubjectService.changeDetector?.markForCheck();
      },
    });
  }
  getPageForRole() {
    switch (this.roles) {
      case 'admin':
        this.navigationPages = this.filterNavigationByRole(
          defaultNavigation,
          Roles.Admin
        );
        break;
      case 'teacher':
        this.navigationPages = this.filterNavigationByRole(
          defaultNavigation,
          Roles.Teacher
        );
        break;
      case 'student':
        this.navigationPages = this.filterNavigationByRole(
          defaultNavigation,
          Roles.Student
        );
        break;
    }
  }

  filterNavigationByRole(
    navigation: NavigationItem[],
    role: Roles
  ): NavigationItem[] {
    return navigation
      .filter((x) => x.key?.includes(role))
      .map((x) => {
        const filteredItem = { ...x };
        if (filteredItem.items) {
          filteredItem.items = this.filterNavigationByRole(
            filteredItem.items,
            role
          );
        }
        return filteredItem;
      });
  }

  getUrlsForRole(navigation: NavigationItem[]): string[] {
    let urls: string[] = [];

    for (const item of navigation) {
      // Check if the current item is accessible by the specified role
      if (item.key!.includes(this.getRoles())) {
        if (item.command) {
          urls.push(item.command); // Add the command URL if it exists
        }

        // If the item has child items, call the function recursively
        if (item.items) {
          const childUrls = this.getUrlsForRole(item.items);
          urls = urls.concat(childUrls); // Combine URLs from children
        }
      }
    }

    return urls;
  }

  CheckNavigationIsForRole(url: UrlSegment[]): boolean {
    let isHas = false;
    this.accessUrlList.forEach((x) => {
      if (url.filter(y => x.includes(y.path)).length!=0 || url.length==0) {        
        isHas = true;
        return;
      }
    });

    
    return isHas;
  }
}
