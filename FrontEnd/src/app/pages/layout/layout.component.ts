import { Component } from '@angular/core';
import { HeaderComponent } from '../../components/header/header.component';
import { RouterOutlet } from '@angular/router';
import { SpinnerComponent } from "../spinner/spinner.component";

@Component({
    selector: 'app-layout',
    imports: [HeaderComponent, RouterOutlet, SpinnerComponent],
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.css'
})
export class LayoutComponent {

}
