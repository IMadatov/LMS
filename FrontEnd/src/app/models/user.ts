import { Status } from "./status";

export interface User {
  countNumber:number;
  id:string|undefined;
  telegramId: string | undefined;
  userName: string | undefined;
  email: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  photoUrl: string | undefined;
  telegramUserName: string | undefined;
  createdDate: Date | undefined;
  active:boolean|undefined;
  photoIsValid:boolean;
  roles:string[]|undefined;
  statusUser:Status;
  strLanguage:string;
}
/**
 {
  "id": "9a135ef7-c617-4171-88de-f7168506761e",
  "telegramId": "6322528596",
  "userName": "Azizjon03",
  "email": "",
  "firstName": "Azizjon",
  "lastName": null,
  "photoUrl": "https://t.me/i/userpic/320/hcCxRAW8Xq-Ma9RKz8_ddmuMEDT0Lr8pze5hZzv9AheLQqchq3xxTaeymEp-ktSO.jpg",
  "telegramUserName": "aliqulov_azizjon",
  "createdAt": "2024-09-25T10:45:09.8159264",
  "roles": []
}
 */
