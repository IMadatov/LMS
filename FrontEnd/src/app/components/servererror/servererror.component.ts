import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-servererror',
    imports: [],
    templateUrl: './servererror.component.html',
    styleUrl: './servererror.component.css'
})
export class ServererrorComponent {
  /**
   *
   */
  constructor(private router:Router) {
  }
  retry(){
    this.router.navigateByUrl('/')
  }
}
