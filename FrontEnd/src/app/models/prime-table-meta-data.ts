import { FilterMetadata } from "primeng/api";

export interface PrimeTableMetaData {
    first?:number;
    rows?:number;
    sortField?:string;
    sortOrder?:number;
    globalFilter?:string|string[]|undefined|null;
    filters?:{[key:string]:FilterMetadata};
}

/*
{
  "first": 0,
  "rows": 10,
  "filters": {
    "additionalProp1": {
      "value": 1,
      "matchMode": "equals"
    },
    "additionalProp2": {
      "value": 1,
      "matchMode": "equals"
    },
    "additionalProp3": {
      "value": 1,
      "matchMode": "equals"
    }
  },
  "sortField": "id",
  "sortOrder": 1,
  "globalFilter": ""
}
*/