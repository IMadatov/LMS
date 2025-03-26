import { Injectable } from '@angular/core';
import { AuthService } from '../../pages/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {


  
  constructor(private authService:AuthService) {}

  

  
  public isActiveSideBarBool: boolean = false;

  ActiveSideBar() {
    this.isActiveSideBarBool = true;
  }


  DeActiveSideBar() {
    this.isActiveSideBarBool = false;
  }

  LogOut() {

    this.authService.signOut().subscribe({
      next: (resp) => {
        location.reload();
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
  
  
}
