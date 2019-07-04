import { share, finalize, flatMap, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { UserHubService } from '@br/communications/hubs';
import { AppUser, Language, UserProfileRequest } from '../models';
import { TranslateService } from '@ngx-translate/core';
import { UserRegistration } from '../models/api-contracts/user-registration.contract';
import { Endpoints } from '@br/config';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(
        private securitySvc: SecurityService,
        private userHub: UserHubService,
        private http: HttpClient,
        public router: Router,
        public translate: TranslateService
    ) { }


    get user() { return this.securitySvc.user$.getValue(); }

    get isLoggedIn() {
        return this.securitySvc.isAuthenticated;
    }

    init() {
        let observabe = of(null).pipe(
            flatMap(() => this.securitySvc.init()),
            flatMap( (val) => {
                // if we have initialized user, start signalR
                if(val) {
                    return this.userHub.init()
                }
                return of(null);
            }),
            share()
        );

        observabe.subscribe(val => {

        })

        return observabe;
    }

    logIn(login: string, password: string, goToProfile?: boolean) {
        const observable = this.securitySvc.login(login, password);
        observable
            .subscribe(data => {
                // start real time communication with server
                this.userHub.init();

                // navigate user depend on role to different pages
                if (goToProfile) {
                    this.router.navigateByUrl(Endpoints.forntend.user.profileUrl);
                    return;
                }

                this.router.navigateByUrl(Endpoints.forntend.reader.dashboardUrl);

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
        let observable = this.securitySvc.registration(model).pipe(
            flatMap(val => {
                return this.logIn(model.username, model.password, true);
            }),
            share());

        observable.subscribe(data => {
                // this.router.navigate(['authorize']);
            }, err => { // error
                console.error(err);
            });

        return observable;
    }

    changeLanguage(lang: Language) {
        this.translate.use(lang.code);
    }

    updateProfile(profile: AppUser) {
        const url = Endpoints.api.user.profile;
        const observable = this.http.put<AppUser>(url, profile).pipe(share());
        return observable;
    }
}
