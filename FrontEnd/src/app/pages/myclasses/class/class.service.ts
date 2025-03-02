import { Injectable } from '@angular/core';
import { Class } from '../../../models/class';

@Injectable({
  providedIn: 'root'
})
export class ClassService {

  currentClass:Class|undefined;
  
  constructor() { }

}
