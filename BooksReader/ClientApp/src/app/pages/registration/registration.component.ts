import { Component, DoCheck } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import {
    AuthorizationService,
    SecurityService,
    NotificationService
} from '../../services';
import { RegistrationModel } from '../../models';

@Component({
    selector: 'app-registration-component',
    templateUrl: './registration.component.html'
})
export class RegistrationComponent implements DoCheck {
    model = {
        username: '',
        password: '',
        passConfirmation: ''
     } as RegistrationModel;

     errMessage = '';

    ngDoCheck(): void {
        this.security.isLoggedIn = false;
    }

    constructor(
        private security: SecurityService,
        private authorization: AuthorizationService,
        private notifications: NotificationService
    ) {
        this.security.isLoggedIn = false;
    }

    registration() {
        if (this.model.passConfirmation !== this.model.password) {
            this.errMessage = 'Password and confirmation are not equal';
            return;
        } else {
            this.errMessage = '';
        }
        this.authorization.registration(this.model.username, this.model.password);
    }
}
