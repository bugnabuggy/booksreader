import { share , finalize } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { UserHubService } from '@br/communications/hubs';
import { AppUser } from '../models';

@Injectable({
        providedIn: 'root'
    })
export class UserService {
    constructor(
        private securitySvc: SecurityService,
        private userHub: UserHubService,
        public router: Router,
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
        const observable = this.securitySvc.login(login, password)
        .pipe(share());

        observable
            .subscribe(data => {
            this.router.navigate(['dashboard']);
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

    registration(login: string, password: string) {
        if (this.actionInProgress) {
            return of();
        }
        this.actionInProgress = true;

        const observable = this.securitySvc.registration(login, password);

        observable.pipe(
            finalize( () => {
            this.actionInProgress = false;
            }))
            .subscribe(data => {
            this.router.navigate(['authorize']);
        }, err => { // error
            console.error(err);
        });
        return observable;
    }
 }
