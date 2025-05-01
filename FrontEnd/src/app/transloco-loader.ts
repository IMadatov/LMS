import { inject, Injectable } from "@angular/core";
import { Translation, TranslocoLoader } from "@jsverse/transloco";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { TranslocoClient } from "./nswag/nswag.translation";

@Injectable({ providedIn: 'root' })
export class TranslocoHttpLoader implements TranslocoLoader {
    private http = inject(HttpClient);
    private translationClient=inject(TranslocoClient);
    getTranslation(lang: string):Observable<Translation> {
        

        

        return this.translationClient.currentLanguage();

        // return this.http.post<Translation>( "https://localhost:7101/api/Transloco/CurrentLanguage?lang="+lang,{},
        //     {
        //     withCredentials:true
        // });
    }
}
