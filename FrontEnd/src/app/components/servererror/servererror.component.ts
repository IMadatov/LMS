import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-servererror',
  standalone: true,
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
