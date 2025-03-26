import { Injectable } from "@angular/core";
import { HashMap, TranslocoMissingHandler, TranslocoMissingHandlerData } from "@jsverse/transloco";
import { debounceTime, distinctUntilChanged, Subject } from "rxjs";
import { TranslocoClient, TranslocoDto } from "./nswag/nswag.translation";



@Injectable({ providedIn: 'root' })
export class TranslocoCustomMissingHandler implements TranslocoMissingHandler{
    constructor(private translocoClient: TranslocoClient) {
        // this.keysSubject
        // .pipe(
        //     debounceTime(1000)
        // )
        // .subscribe((keys) => {
        //     this.keys=keys;
        //     console.log(keys);
            
        // });
    }

    

    keys: TranslocoDto[] = [];

    keysSubject = new Subject<TranslocoDto[]>();

    handle(key: string, data: TranslocoMissingHandlerData, params?: HashMap) {
        this.keys.push({ code: key, valueEN: key, valueKR: key, valueRU: key, valueUZ: key, id: 0 } as TranslocoDto);
        console.log(this.keys);
        
    }


    RefreshKeys(){
        this.translocoClient.insertAuto(this.keys).subscribe({
            next:(value)=>{
                console.log("refreshed");
                this.keys=[];
    }})
    }

}