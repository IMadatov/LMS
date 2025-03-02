import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {


  constructor() { }
  isloading=new BehaviorSubject<boolean>(false);


  setLoading(onload:boolean){
    this.isloading.next(onload);
  }

}
