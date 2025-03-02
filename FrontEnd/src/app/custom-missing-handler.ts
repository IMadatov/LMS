import { inject, Injectable } from "@angular/core";
import { HashMap, TranslocoMissingHandler, TranslocoMissingHandlerData, TranslocoService } from "@jsverse/transloco";
import { HttpService } from "./services/http.service";



@Injectable({providedIn:'root'})
export class CustomMissingHandler implements TranslocoMissingHandler{

    httpService=inject(HttpService);
    
    handle(key: string, data: TranslocoMissingHandlerData, params?: HashMap) {
        this.SaveMissingTranslations(key);
    }

    SaveMissingTranslations(key:string){
        this.httpService.InsertWordTransloco({ id:0,code:key,valueEN:key,valueKR:key,valueRU:key,valueUZ:key}).subscribe();
    }

}