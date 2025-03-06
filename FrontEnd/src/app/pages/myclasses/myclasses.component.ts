import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MyclassesService } from './myclasses.service';
import { InputNumberModule } from 'primeng/inputnumber';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaginatorModule } from 'primeng/paginator';
import { AuthService } from '../auth/auth.service';
import { TranslocoDirective, TranslocoModule } from '@jsverse/transloco';
@Component({
    selector: 'app-myclasses',
    imports: [
        PaginatorModule,
        ReactiveFormsModule,
        TableModule,
        CommonModule,
        PanelModule,
        ButtonModule,
        DialogModule,
        InputNumberModule,
        FloatLabelModule,
        InputTextModule,
        TranslocoModule,
        TranslocoDirective
    ],
    templateUrl: './myclasses.component.html',
    styleUrl: './myclasses.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    standalone: true
})
export class MyclassesComponent implements OnInit, OnDestroy {


  constructor(
    public myclassesService: MyclassesService,
    private cd: ChangeDetectorRef,
    public authService: AuthService
  ) {
    this.myclassesService.cd = cd;

  }

  ngOnInit() {
    this.myclassesService.countMyClassitems = 0;
  }

  sorting(event: any) {
    console.log(event);

  }


  createClass = new FormGroup({
    ClassName: new FormControl('', [
      Validators.minLength(3),
      Validators.required
    ]),
    ClassDagree: new FormControl('', [
      Validators.min(1),
      Validators.required
    ])
  });

  CreateClass() {
    this.myclassesService.CreateClass(this.createClass.value.ClassName?.toString()!, this.createClass.value.ClassDagree?.toString()!);
  }

  ngOnDestroy(): void {
    this.myclassesService.cd = null;
  }

  Check():boolean{
    return this.myclassesService.countMyClassitems==this.myclassesService.selectionClass.length;
    
  }
}
