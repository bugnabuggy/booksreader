import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { TranslateService } from '@ngx-translate/core';
import { getErrorMessage } from '@br/utilities/error-extractor';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  messages: any =
    {
      notifications: [],
      errors: [],
      successes: []
    };

  constructor(
    private notifications: NotificationsService,
    private translate: TranslateService
  ) { }

  showNotification(message: string, buttonText: string, type) {
    //throw new Error('Not implemented');
  }

  showError(message: any, defaultMessage?: string) {
    // this.messages['errors'].push({ message, buttonText });
    let msg = typeof message === 'object' 
    ? getErrorMessage(message)
    : message;  
      
    this.notifications.error('', msg);
  }

  showInfo(message: string) {
    // this.messages['notifications'].push({ message, buttonText });
    this.notifications.info('', message);
  }

  showSuccess(message: string, buttonText: string = 'Ok') {
    // this.messages['successes'].push({ message, buttonText });
    this.notifications.success('', message);
  }
}
