import { share , finalize, flatMap } from 'rxjs/operators';

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
        public translate:TranslateService
       ) {
    }


    user: AppUser = null;
    
    // TODO: refactor this ↓ because it is a source of bugs
    actionInProgress: boolean = false;

    get isLoggedIn() {
        return this.securitySvc.isLoggedIn;
    }

    init() {
        this.securitySvc.init();
    }

    logIn(login: string, password: string) {
        if (this.actionInProgress) {
            return of();
        }
        this.actionInProgress = true;
        const observable = this.securitySvc.login(login, password);

        observable
            .subscribe(data => {
            debugger;

            // navigate user depend on role to different pages

            this.router.navigate(Endpoints.forntend.user.profile.split('/'));
        }, err => { // error
            console.error(err);
        });

        return observable.pipe(finalize( () => {
            this.actionInProgress = false;
        }));
    }

    externalLogIn(type: string, access_token: string) {
        if (this.actionInProgress) {
            return of(null);
        }

        this.actionInProgress = true;
        const observable = this.securitySvc.externalLogin(type, access_token);

        observable.subscribe(c=>{
            this.actionInProgress = false;
        })


        return observable;
    }

    logOut() {
        this.securitySvc.clearTokens();
        this.userHub.stop();
        this.router.navigate(['authorize']);
    }

    registration(model: UserRegistration) {
        if (this.actionInProgress) {
            return of();
        }
        this.actionInProgress = true;

        const observable = this.securitySvc.registration(model);

        observable.pipe(
            finalize( () => {
            this.actionInProgress = false;
            }),
            flatMap(val => {
                this.actionInProgress = false;
                return this.logIn(model.username, model.password)
            }))
            .subscribe(data => {
                // this.router.navigate(['authorize']);
        }, err => { // error
            console.error(err);
        });
        return observable;
    }

    changeLanguage(lang: Language){
        this.translate.use(lang.code);
    }
 }
