import { Route } from "@angular/router";
import { SubjectsComponent } from "./subjects.component";
import { SubjectComponent } from "./subject/subject.component";

export const SUBJECTS_ROUTES:Route[]=[
    {
        path:'',
        component:SubjectsComponent
    },
    {
        path:'subject',
        component:SubjectComponent
    }
]