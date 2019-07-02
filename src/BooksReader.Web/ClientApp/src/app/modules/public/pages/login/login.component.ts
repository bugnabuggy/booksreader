import { Component, DoCheck, OnInit } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import {
    UserService,
    SecurityService,
    NotificationService
} from '@br/core/services';

import { SocialLoginService } from '@br/integrations/services';
import { LoginModel } from '@br/core/models';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    

    errorMessage = '';
    loggedInWithFacebook = false;

    loginForm = this.fb.group({
        username: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(3)]],
    })


    constructor(
        private authorization: UserService,
        private notifications: NotificationService,
        public socialSvc: SocialLoginService,
        private fb: FormBuilder
    ) {
    }

    ngOnInit() {
        this.socialSvc.init();
        this.socialSvc.user$.subscribe(user => {
        });
    }

    login() {
        const loginModel = this.loginForm.value as LoginModel;
        this.authorization.logIn(loginModel.username, loginModel.password)
            .subscribe((data) => {

            }, (err) => {
                debugger;
                this.errorMessage = err.error.error_description;
                if (err.status === 400) {
                    this.notifications.showError(`Invalid username or password`);
                }
            });
    }

    socialLogin() {
        this.socialSvc.signInWithFB().subscribe(user => {
            this.loggedInWithFacebook = true;
            debugger;
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
