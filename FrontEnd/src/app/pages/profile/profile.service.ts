import { Injectable } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { User } from '../../models/user';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  public userData: User | undefined;
  defaultAvatar: string | undefined =
    '/account_circle_24dp_E8EAED_FILL0_wght400_GRAD0_opsz24.svg';

  constructor(
    private httpService: HttpService,
    private authService:AuthService,
    private toastr: ToastrService
  ) {}

  getUserData() {
    this.httpService.AboutMe().subscribe({
      next: (resp) => {
        this.userData = resp;
        this.authService.currentUser=resp;
        console.log(resp.statusUser);
        
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  loading = false;
  Reload() {
    this.loading = true;
    this.httpService.ReloadUserInfo().subscribe({
      next: (resp) => {
        if (resp) this.toastr.info('Data refreshed by Telegram');
        this.getUserData();
      },
      error:err=>console.error(err),
      complete:()=>{
        this.loading=false;
      }
    });
  }
  
}
