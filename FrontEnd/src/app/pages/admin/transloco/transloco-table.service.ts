import { ChangeDetectorRef, Injectable } from '@angular/core';
import { HttpService } from '../../../services/http.service';
import { Transloco } from '../../../models/transloco';
import { ToastrService } from 'ngx-toastr';
import { TableLazyLoadEvent } from 'primeng/table';
import { Translation, TranslocoService } from '@jsverse/transloco';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslocoTableService {


  //Prime table fields
  first?:number=0;
  rows?:number=5;
  totalItems:number=0;
  globalFilter:string|string[]|null|undefined;
  sortField:string='code';
  cloneedProducts:{[id:number]:Transloco}={};
  order=1;

  //Subject for global search input waiting
  public searchSubject=new Subject<string>();
  
  //Transloco model
  translocoList:Transloco[]|undefined;

  translocoNewItem:Transloco|undefined;
  translocoNewItemCode:string|undefined;
  translocoNewItemValueEN:string|undefined;
  translocoNewItemValueRU:string|undefined;
  translocoNewItemValueUZ:string|undefined;
  translocoNewItemValueKR:string|undefined;


  constructor(
    private httpService:HttpService,
    private toastrService:ToastrService,
    private translocoService:TranslocoService
  ) { }

  GetTranslocoItems(){
    this.httpService.GetTranslations({first:this.first,rows:this.rows,sortField:this.sortField,sortOrder:this.order,globalFilter:this.globalFilter}).subscribe({
      next:(value)=>{
        this.translocoList=value.items;
        // this.translocoList?.push({code:"",valueEN:"  ",valueKR:" ",valueRU:"  ",valueUZ:"  ",id:0});
        this.totalItems=value.totalItems||0;    
      }
    })

  }
  onRowEditInit(translocoItem:Transloco){
    if(translocoItem.id!==undefined)
    this.cloneedProducts[translocoItem.id!] = { ...translocoItem };

  }
  onRowEditSave(translocoItem:Transloco){

    
    if(translocoItem.code?.length!=0){
      
      this.httpService.InsertWordTransloco(translocoItem).subscribe({
        next:(value)=>{
          this.GetTranslocoItems();

          this.toastrService.success('Transloco item saved');
        }
      })
    }
    // this.translocoList?.pop();
    // this.translocoList?.push({code:"",valueEN:"  ",valueKR:" ",valueRU:"  ",valueUZ:"  ",id:0});
    
    delete this.cloneedProducts[translocoItem.id!];
  }
  onRowEditCancel(translocoItem:Transloco,index:number){
    if(translocoItem.id!=undefined){
      
      if(this.translocoList?.at(index)!=undefined){
        this.translocoList[index]= this.cloneedProducts[translocoItem.id!];
      }
    delete this.cloneedProducts[translocoItem.id!];
    }
  }

  onRowDelete(translocoItem:Transloco){
    this.httpService.DeleteWordTransloco(translocoItem).subscribe(()=>{
      this.GetTranslocoItems();
      this.toastrService.show("Deleted")
    });
    delete this.cloneedProducts[translocoItem.id!];
  }


  onPageChange($event:TableLazyLoadEvent){
    
    this.first=$event.first;
    this.rows=$event.rows||5;
    this.order=$event.sortOrder||1;
    this.sortField=$event.sortField?.toString()||'code';
    this.globalFilter=$event.globalFilter;
    // console.log($event);
    
    this.GetTranslocoItems();
  }


  
}
