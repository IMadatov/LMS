import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { Login } from '../models/login';
import { Signup } from '../models/signup';
import { User } from '../models/user';
import { ChangeRole } from '../models/change-role';
import { PaginatedList } from '../models/paginated-list';
import { Router } from '@angular/router';
import { Subject } from '../models/subject';
import { PrimeTableMetaData } from '../models/prime-table-meta-data';
import { Language } from '../models/language';
import { Transloco } from '../models/transloco';


@Injectable({
  providedIn: 'root',
})
export class HttpService {





  
  urlGlobal = 'https://8hgnk3gt-7101.inc1.devtunnels.ms/';
  urlLocal = 'https://localhost:7101/';
  urlBase = '';
  isAuthenticated$: ReplaySubject<boolean> = new ReplaySubject(1);

  url = this.urlGlobal + this.urlBase;

  constructor(private http: HttpClient, private router: Router) { }





  CheckUserName(name: string): Observable<boolean> {
    return this.http.get<boolean>(
      this.url + 'api/Auth/CheckUsername?username=' + name,
      {
        withCredentials: true,
      }
    );
  }

  Login(login: Login): Observable<boolean> {
    return this.http.post<boolean>(this.url + 'api/Auth/SignIn', login, {
      withCredentials: true,
    });
  }
  Logout(): Observable<boolean> {
    return this.http.post<boolean>(
      this.url + 'api/Auth/SignOut',
      {},
      {
        withCredentials: true,
      }
    );
  }
  OnSite(): Observable<string> {
    return this.http.get<string>(this.url + 'api/Auth/OnSite', {
      withCredentials: true,
      responseType: "text" as any
    });
  }
  LoginAsTelegram(telegramData: string): Observable<boolean> {
    return this.http.post<boolean>(
      this.url + 'api/Auth/CheckTelegramData?telegramData=' + telegramData,
      {},
      {
        withCredentials: true,
      },
    );
  }

  SignUpAsTelergam(userData: Signup, telegramData: string): Observable<boolean> {
    return this.http.post<boolean>(
      this.url + 'api/Auth/SignUpWithTelegram?telegramData=' + telegramData, userData, {
      withCredentials: true
    }
    )
  }

  AboutMe(): Observable<User> {
    return this.http.get<User>(this.url + 'api/User/Me', {
      withCredentials: true
    })
  }

  CheckAvatar(url: string): Observable<any> {
    const headers = new HttpHeaders().set('Accept', 'image/avif,image/webp,image/png,image/svg+xml,image/*;q=0.8,*/*;q=0.5');

    return this.http.get(url, {
      withCredentials: true,
      headers: headers
    });
  }



  //Admin
  GetAllUsers(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/User/GetAllUsers', primeTableMetaData, {
      withCredentials: true
    })
  }

  GetAdmins(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/User/GetAdmins', primeTableMetaData, {
      withCredentials: true
    });
  }

  GetTeachers(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/User/GetTeacherUsers', primeTableMetaData, {
      withCredentials: true
    })
  }

  GetStudents(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/User/GetStudents', primeTableMetaData, {
      withCredentials: true,
    })
  }


  UpdataRoleUser(selectedRole: ChangeRole): Observable<boolean> {
    return this.http.put<boolean>(this.url + 'api/Admin/UpdateRole', selectedRole, {
      withCredentials: true
    });
  }

  //Teacher
  GetClasses(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/Teacher/GetAllClass', primeTableMetaData, {
      withCredentials: true
    })
  }
  //https://localhost:7101/api/Teacher/CreateClass
  DeleteClasses(classes: number[]): Observable<boolean> {
    return this.http.delete<boolean>(this.url + 'api/Teacher/DeleteClasses', {
      body: classes,
      withCredentials: true
    })
  }

  CreateClass(name: string, dagree: number): Observable<boolean> {
    return this.http.post<boolean>(this.url + 'api/Teacher/CreateClass', {
      "dagree": dagree,
      "name": name
    }, {
      withCredentials: true
    })
  }

  GetSubjects(primeTableMetaData: PrimeTableMetaData): Observable<PaginatedList> {
    return this.http.post<PaginatedList>(this.url + 'api/Teacher/GetSubjects', primeTableMetaData, {
      withCredentials: true
    })
  }
  CreateSubject(subject: Subject): Observable<boolean> {
    return this.http.post<boolean>(this.url + 'api/Teacher/CreateSubject', subject, {
      withCredentials: true
    })
  }

  DeleteSubject(subjects: Subject[]): Observable<boolean> {

    return this.http.delete<boolean>(this.url + 'api/Teacher/DeleteSubjects', {
      body: subjects,
      withCredentials: true
    })
  }

  //Profile
  ReloadUserInfo(): Observable<boolean> {
    return this.http.get<boolean>(this.url + 'api/TelegramBot/RefreshUserInfo', {
      withCredentials: true
    })
  }

  UserLanguage(): Observable<Language> {
    return this.http.get<Language>(this.url + "api/User/GetUserLanguage", {
      withCredentials: true,
      
    });
  }

  ChangeLanguage(lang: string): Observable<Language> {
    return this.http.post<Language>(this.url + "api/User/ChangeLanguage?lang="+ lang,{}, {
      withCredentials: true
    });
  }

  //transloco

  GetTranslations(primeTableMetaData:PrimeTableMetaData):Observable<PaginatedList>{
    return this.http.post<PaginatedList>(this.url+'api/Transloco/GetTranslations',primeTableMetaData,{
      withCredentials:true
    })
  }

  InsertWordTransloco(transloco:Transloco):Observable<Transloco>{
    console.log("Insert method");
    
    return this.http.post<Transloco>(this.url+'api/Transloco/InsertOrUpdateWord',transloco,{
      withCredentials:true
    });
  }

  DeleteWordTransloco(transloco:Transloco):Observable<boolean>{
    return this.http.delete<boolean>(this.url+'api/Transloco/DeleteWord?id='+transloco.id,{
      withCredentials:true,
    })
  }

}
