import { User } from "./user";

export interface Subject {
    id?:number|undefined|null;
    name?:string|undefined|null;
    craetedDate?:Date|undefined|null;
    userId?:string|undefined|null;
    indexOfItem?:number|undefined|null;
    user?:User|undefined|null;
    creator?:string;
}
/**
 * 
 *   {
      "id": 6,
      "name": "Matematika",
      "atCreate": "2024-10-08T17:34:44.0799009",
      "userId": "17cdac85-2ee0-4440-9db7-96c069dc92aa"
    }
 */