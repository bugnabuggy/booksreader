import { Component, DoCheck, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import {
    UserService,
    SecurityService,
    NotificationService
} from '../../../services';

import { SocialLoginService } from '@br/integrations/services';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loggedInWithFacebook = false;
    userNameFormControl = new FormControl('', [
        Validators.required
    ]);

    passwordFormControl = new FormControl('', [
        Validators.required
    ]);


    constructor(
        private security: SecurityService,
        private authorization: UserService,
        private notifications: NotificationService,
        public socialSvc: SocialLoginService
    ) {
        this.security.isLoggedIn = false;
    }

    ngOnInit() {
        this.socialSvc.init();
        this.socialSvc.user$.subscribe(user => {

        });
    }

    login() {
        this.authorization.logIn(this.userNameFormControl.value, this.passwordFormControl.value)
            .subscribe((data) => {

            }, (err) => {
                if (err.status === 400) {
                    this.notifications.showError(`Invalid login or password`);
                }
            });
    }

    socialLogin() {
        this.socialSvc.signInWithFB().subscribe(user => {
            this.loggedInWithFacebook = true;
            this.authorization.externalLogIn('Facebook', user.authToken)
            .subscribe(val => {
                console.log(val);
                // redirect or ask for protected endpoint
            });
            console.log(user);
        });
    }

    socialLogout() {
        this.socialSvc.signOutWithFB().subscribe(val => {
            this.loggedInWithFacebook = false;
            console.log(val);
        });
    }
}
