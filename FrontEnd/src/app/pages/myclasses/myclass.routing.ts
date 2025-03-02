import { Route } from "@angular/router";
import { MyclassesComponent } from "./myclasses.component";
import { ClassComponent } from "./class/class.component";

export const MyClassRoute:Route[]=[
    {
        path:'',
        component:MyclassesComponent,
    },
    {
        path:'class/:id',
        component:ClassComponent
    }
]