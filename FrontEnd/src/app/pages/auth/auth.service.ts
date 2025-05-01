import { Injectable } from '@angular/core';
import { NavigationItem } from '../../models/navigation-item';
import { defaultNavigation } from './navigations';
import { Roles } from '../../models/roles';
import { SignalrService } from '../../services/signalr.service';
import { Router, UrlSegment } from '@angular/router';
import { MySubjectService } from '../my-subject/my-subject.service';
import { AuthClient, JWTTokenModel, SignInDto, UserClient, UserDto } from '../../nswag/nswag.auth';
import { catchError, map, Observable, of, ReplaySubject, throwError } from 'rxjs';
import { ErrorHandlerService } from '../../services/error-handler.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoading: boolean = false;
  CurrentUser: UserDto | undefined;
  CurrentRole: string | undefined;
  CurrentRoles: string[] | undefined;
  navigationPages: NavigationItem[] | undefined;

  jwtToken: JWTTokenModel | undefined;
  private _authenticated: boolean = false;
  private _authenticated$: ReplaySubject<boolean> = new ReplaySubject(1);



  accessUrlList: string[] = [];


  constructor(
    private userClient: UserClient,
    private signalRService: SignalrService,
    private mysubjectService: MySubjectService,
    private authClient: AuthClient,
    private errorHandlerService: ErrorHandlerService,
    private router: Router,

  ) {

  }
  signIn(signInDto: SignInDto): Observable<boolean> {

    if (this._authenticated) {
      return throwError(() => new Error("Already authenticated"));
    }

    return this.authClient.signIn(signInDto).pipe(
      map((token: JWTTokenModel) => {
        this.jwtToken = token;
        this.accessToken = token.accessToken!;
        this.refreshToken = token.refreshToken!;
        this._authenticated = true;
        this._authenticated$.next(true);
        return true;
      }),
      catchError((err: any) => {
        this._authenticated = false;
        this._authenticated$.next(false);

        this.errorHandlerService.handleError(err);

        return of(false);
      })
    )
  }

  set accessToken(token: string) {
    sessionStorage.removeItem("accessToken");
    sessionStorage.setItem("accessToken", token);
  }

  get accessToken(): string {
    return sessionStorage.getItem("accessToken")!;
  }

  set refreshToken(token: string) {
    sessionStorage.removeItem("refreshToken");
    sessionStorage.setItem("refreshToken", token);
  }
  get refreshToken(): string {
    return sessionStorage.getItem("refreshToken")!;
  }

  get authenticated$(): Observable<boolean> {
    return this._authenticated$.asObservable();
  }


  signOut() {

    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("refreshToken");
    this._authenticated = false;
    this._authenticated$.next(false);
    // location.reload();
  }



  Reload() {
    console.log("reload");

  }


  getRoles(): any {
    switch (this.CurrentRole) {
      case 'admin':
        return Roles.Admin;
      case 'teacher':
        return Roles.Teacher;
      case 'student':
        return Roles.Student;
      case 'user':
        return Roles.User
    }
  }

  getCurrentUser() {
    this.signalRService.connect();

    this.userClient.me().subscribe({
      next: (resp) => {
        this.CurrentUser = resp;
        this.CurrentRole = resp.mainRole;
        this.CurrentRoles = resp.roles;
        this._authenticated = true;
        this._authenticated$.next(true);
        this.mysubjectService.changeDetector?.markForCheck();
      },
    });

    // this.httpService.AboutMe().subscribe({
    //   next: (resp) => {
    //     this.currentUser = resp;

    //     this.mysubjectService.changeDetector?.markForCheck();
    //   },
    // });
  }
  getPageForRole() {
    switch (this.CurrentRole) {
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

    this.accessUrlList = this.getUrlsForRole(this.navigationPages!);


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
      if (url.filter(y => x.includes(y.path)).length != 0 || url.length == 0) {
        isHas = true;
        return;
      }
    });


    return isHas;
  }
}
