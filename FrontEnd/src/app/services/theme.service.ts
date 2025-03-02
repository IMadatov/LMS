import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService{

  isDarkMode = false;
  themeName:string='lara';
  themeColor:string='blue';
  constructor() { }

  loadTheme(){
    this.isDarkMode=localStorage.getItem('theme')==='dark'?true:false;
    this.setTheme();
  }

  setTheme(){
    const linkHtmlElement = document.getElementById('theme') as HTMLLinkElement;
    document.body.classList.toggle('dark',this.isDarkMode);
    linkHtmlElement.href=this.getThemePath();
  }

  toolbarToggle(){
    this.isDarkMode=!this.isDarkMode;
    localStorage.setItem('theme',this.isDarkMode?'dark':'light');
    this.setTheme();
  }

  getThemeMode(){
    return this.isDarkMode;
  }

  setThemeMode(value:boolean){
    this.isDarkMode=value;
    localStorage.setItem('theme',this.isDarkMode?'dark':'light');
  }

  getThemePath():string{
    return `primeng/themes/${this.themeName}-${this.isDarkMode?'dark':'light'}-${this.themeColor}/theme.css`;
  }
}
