import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ServiceError } from '../models/service-error';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  response:ServiceError[]=[];

  httpDefaultError:HttpErrorResponse|undefined;

  constructor(private toastrService:ToastrService) { }


  set error(error:any){
    this.error=error;
  }

  get error(){
    return this.error;
  }

  handleError(error:any){
    this.httpDefaultError=error as HttpErrorResponse;

    this.response=JSON.parse(error.response);
    
    this.showError();
  }

  showError(){
    if(this.httpDefaultError?.status!>=400){  
          this.toastrService.error(this.response[0].errorMessage,this.response[0].errorKey);
    }
  }
}
