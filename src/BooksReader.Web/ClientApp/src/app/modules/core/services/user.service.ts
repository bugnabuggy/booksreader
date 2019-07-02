import { share, finalize, flatMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { UserHubService } from '@br/communications/hubs';
import { AppUser, Language } from '../models';
import { TranslateService } from '@ngx-translate/core';
import { UserRegistration } from '../models/api-contracts/user-registration.contract';
import { Endpoints } from '@br/config';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(
        private securitySvc: SecurityService,
        private userHub: UserHubService,
        public router: Router,
        public translate: TranslateService
    ) { }


    get user() { return this.securitySvc.user$.getValue(); }

    get isLoggedIn() {
        return this.securitySvc.isAuthenticated;
    }

    init() {
        this.securitySvc.init();
    }

    logIn(login: string, password: string, goToProfile?: boolean) {
        const observable = this.securitySvc.login(login, password);

        observable
            .subscribe(data => {

                // navigate user depend on role to different pages
                if (goToProfile) {
                    this.router.navigateByUrl(Endpoints.forntend.user.profileUrl);
                    return;
                }
            }, err => { // error
                console.error(err);
            });

        return observable;
    }

    externalLogIn(type: string, access_token: string) {
        const observable = this.securitySvc.externalLogin(type, access_token);
        return observable;
    }

    logOut() {
        this.userHub.stop();
        this.securitySvc.clearTokens();
        this.router.navigate([Endpoints.forntend.main]);
    }

    registration(model: UserRegistration) {
        const observable = this.securitySvc.registration(model);

        observable.pipe(
            flatMap(val => {
                return this.logIn(model.username, model.password, true);
            }))
            .subscribe(data => {
                // this.router.navigate(['authorize']);
            }, err => { // error
                console.error(err);
            });

        return observable;
    }

    changeLanguage(lang: Language) {
        this.translate.use(lang.code);
    }
}
