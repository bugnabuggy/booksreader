import { Component, DoCheck, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import {
    UserService,
    SecurityService,
    NotificationService,
} from '@br/core/services';

import { RegistrationModel } from '@br/core/models';
import { UserRegistration } from '@br/core/models/api-contracts/user-registration.contract';

@Component({
    selector: 'app-registration-component',
    templateUrl: './registration.component.html'
})
export class RegistrationComponent implements OnInit {
    model = {
        username: '',
        fullname: '',
        password: '',
        passConfirmation: '',
        agree: false
     } as RegistrationModel;

     errMessage = '';
     serverMessage = '';

    constructor(
        private security: SecurityService,
        private userSvc: UserService,
        private notificatins: NotificationService
    ) {

    }

    ngOnInit(): void {
        
    }

    registration() {
        if (this.model.passConfirmation !== this.model.password) {
            this.errMessage = 'Password and confirmation are not equal';
            return;
        } else {
            this.errMessage = '';
        }

        let model = {
            username: this.model.username,
            fullname: this.model.fullname,
            password: this.model.password,
        } as UserRegistration;

        this.userSvc.registration(model)
        .subscribe(val => {

        }, err => {
            debugger;
            if(err.status == 400) {
                this.serverMessage = err.error;
                return;
            }
            this.notificatins.showError(err.message);
        });
    }
}
