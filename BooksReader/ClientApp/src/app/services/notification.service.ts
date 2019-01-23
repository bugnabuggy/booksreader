import { Injectable } from '@angular/core';

@Injectable()
export class NotificationService {
    messages: any =
        {
            notifications: [],
            errors: [],
            successes: []
        };

    constructor() { }

    showNotification(message: string, buttonText: string, type) {
        throw new Error('Not implemented');
    }

    showError(message: string, buttonText: string = 'Ok') {
        this.messages['errors'].push({ message, buttonText });
    }

    showInfo(message: string, buttonText: string = 'Ok') {
        this.messages['notifications'].push({ message, buttonText });
    }

    showSuccess(message: string, buttonText: string = 'Ok') {
        this.messages['successes'].push({ message, buttonText });
    }
}
