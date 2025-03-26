import { inject, Injectable } from "@angular/core";
import { Translation, TranslocoLoader } from "@jsverse/transloco";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";

@Injectable({ providedIn: 'root' })
export class TranslocoHttpLoader implements TranslocoLoader {
    private http = inject(HttpClient);

    getTranslation(lang: string):Observable<Translation> {
        
        return of([]);

        // return this.http.post<Translation>( "https://localhost:7101/api/Transloco/CurrentLanguage?lang="+lang,{},
        //     {
        //     withCredentials:true
        // });
    }
}
