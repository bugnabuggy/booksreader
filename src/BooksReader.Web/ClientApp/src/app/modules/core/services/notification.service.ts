import { Injectable } from '@angular/core';
import { NotificationsService, NotificationType } from 'angular2-notifications';
import { NotificationKind } from 'rxjs';

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
        private notifications: NotificationsService
    ) { }

    showNotification(message: string, buttonText: string, type) {
        //throw new Error('Not implemented');
    }

    showError(message: string) {
        // this.messages['errors'].push({ message, buttonText });
        this.notifications.error('', message);
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
