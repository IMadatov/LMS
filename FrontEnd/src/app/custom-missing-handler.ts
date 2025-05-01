import { Injectable } from "@angular/core";
import { HashMap, TranslocoConfig, TranslocoMissingHandler, TranslocoMissingHandlerData } from "@jsverse/transloco";
import { debounceTime, distinctUntilChanged, Subject } from "rxjs";
import { TranslocoClient, TranslocoDto } from "./nswag/nswag.translation";
import { ToastrService } from "ngx-toastr";

 /**
  * Custom missing handler for Transloco.
  * This handler is used to handle missing translations in the application.
  * There are some bugs with Missing Handler, when it misses the key, it has been returned two keys as Prefix
  */

@Injectable({ providedIn: 'root' })
export class TranslocoCustomMissingHandler implements TranslocoMissingHandler{
    constructor(private translocoClient: TranslocoClient,
        private _toastr:ToastrService
    ) {
        this.keysSubject
        .pipe(
            debounceTime(1000)
        )
        .subscribe((keys) => {
            

            this.keysList = [...this.keysList, ...keys];
            //Key list dublicates are removed
            this.keysList=this.keysList.slice(0,this.keysList.length/2);
            console.log(this.keysList);

            translocoClient.insertAuto(this.keysList).subscribe({
                next:(value) => {
                    this._toastr.info("Keys Inserted")
                }
            });

            this.keys=[];
            
        });
    }

    

    keys: TranslocoDto[] = [];
    keysList: TranslocoDto[] = [];

    keysSubject = new Subject<TranslocoDto[]>();

    handle(key: string, config:TranslocoConfig) {
        this.keys.push({
            code: key,
            valueEN: key,
            valueUZ: key,
            valueRU: key,
            valueKR: key,
            id:0,
        } as TranslocoDto);
        this.keysSubject.next(this.keys);
        // console.log(key);        
    }


    RefreshKeys(){
        this.translocoClient.insertAuto(this.keysList).subscribe({
            next:(value)=>{
            this._toastr.info("Keys Inserted")
    }})
    }

}