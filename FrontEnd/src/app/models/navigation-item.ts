import { Roles } from "./roles";

export interface NavigationItem {
    key?:Roles[];
    label?:string|undefined;
    icon?:string|undefined;
    command?:string|undefined;
    items?:NavigationItem|undefined|any;
    hidden?:boolean;
}
