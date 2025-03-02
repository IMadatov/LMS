import { Injectable, OnInit } from '@angular/core';
import { HttpService } from '../../services/http.service';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {


  
  constructor(private httpService: HttpService) {}

  

  
  public isActiveSideBarBool: boolean = false;

  ActiveSideBar() {
    this.isActiveSideBarBool = true;
  }


  DeActiveSideBar() {
    this.isActiveSideBarBool = false;
  }

  LogOut() {

    this.httpService.Logout().subscribe({
      next: (resp) => {
        location.reload();
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
  
  
}
