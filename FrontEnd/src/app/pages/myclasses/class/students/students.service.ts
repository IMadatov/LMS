import { Injectable } from '@angular/core';
import { Class } from '../../../../models/class';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  currentClass:Class|undefined

  constructor() { }
  
}
