


/// Don't touch nothing!!!! 

import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Inject,
  Input,
  NgZone,
  Output,
  ViewChild
} from '@angular/core';
import {CommonModule, DOCUMENT} from '@angular/common';
import {UserTelegramData, WidgetConfiguration} from '../../../../models/types';
import { TelegramUserData } from '@telegram-auth/server';

const TELEGRAM_WIDGET_VERSION = 22;

@Component({
    selector: 'app-telegram-login-widget',
    imports: [CommonModule],
    template: `<div  #scriptContainer></div>`,
    styles: [`
    .tgme_widget_login.large button.tgme_widget_login_button {

  }
    
    `]
})
export class TelegramLoginWidgetComponent implements AfterViewInit {

  @ViewChild('scriptContainer', {static: true}) scriptContainer: ElementRef|any;

  @Output() login: EventEmitter<TelegramUserData> = new EventEmitter<TelegramUserData>();
  @Output() load: EventEmitter<void> = new EventEmitter<void>();
  @Output() loadError: EventEmitter<void> = new EventEmitter<void>();

  @Input() botName: string="";
  @Input() config?: any|WidgetConfiguration = {};

  private readonly window: Window|any;
  private readonly document: Document;

  private defaultConfigs = {
    src: `https://telegram.org/js/telegram-widget.js?${TELEGRAM_WIDGET_VERSION}`,
    'data-onauth': `onTelegramLogin(user)`,
    'onerror': `onTelegramWidgetLoadFail()`,
    'onload': `onTelegramWidgetLoad()`
  };

  constructor(
    private ngZone: NgZone,
    @Inject(DOCUMENT) document: any
  ) {
    this.window = window;
    this.document = document as Document
  }

  ngAfterViewInit() {
    const scriptAttrs:any = this.compileConfigs();
    const script = this.document.createElement('script');

    for (let key in scriptAttrs) {
      if (scriptAttrs.hasOwnProperty(key)) {
        script.setAttribute(key, scriptAttrs[key]);
      }
    }
    script.setAttribute('lang','eng');
    
    this.window['onTelegramLogin'] = (data:TelegramUserData) => { 
      this.ngZone.run(() => this.login.emit(data))
      
    };
    this.window['onTelegramWidgetLoad'] = () => this.ngZone.run(() => this.load.emit());
    this.window['onTelegramWidgetLoadFail'] = () => this.ngZone.run(() => this.loadError.emit());

    this.scriptContainer.nativeElement.innerHTML = '';
    this.scriptContainer.nativeElement.appendChild(script);
  }

  private compileConfigs(): object {
    const configs:any = this.defaultConfigs ?? {};

    if (!this.botName) {
      throw new Error('Telegram widget: bot name not present!');
    }

    configs['data-telegram-login'] = this.botName

    if (this.config?.accessToWriteMessages) {
      configs['data-request-access'] = 'write';
    }

    if (this.config?.cornerRadius) {
      configs['data-radius'] = `${this.config.cornerRadius}`;
    }

    if (this.config?.showUserTelegramDataPhoto === false) {
      configs['data-UserTelegramDatapic'] = 'false';
    }

    if (this.config?.buttonStyle) {
      configs['data-size'] = this.config.buttonStyle;
    } else {
      configs['data-size'] = 'large';
    }

    return configs;
  }
}
