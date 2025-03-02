import { Directive, ElementRef, HostBinding, HostListener, Input } from '@angular/core';

@Directive({
  selector: 'img[defaultAvatar]',
  standalone: true,
  host:{
    '(error)':'updateURL()',
    '[src]':'src'
  }
})
export class InvalidAvatarDirective {

  @Input() src:string|undefined;
  @Input() defaultAvatar:string|undefined;

  updateURL(){
    
    this.src=this.defaultAvatar;
  }



}
