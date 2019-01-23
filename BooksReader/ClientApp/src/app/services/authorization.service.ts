import { share , finalize } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Endpoints } from '../enums/Endpoints';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

@Injectable()
export class AuthorizationService {
    constructor(private http: HttpClient,
        private securitySvc: SecurityService,
        public router: Router,
       ) {
    }

    loginInProgress: boolean = false;

    logIn(login: string, password: string) {
        if (this.loginInProgress) {
            return of();
        }

        this.loginInProgress = true;
        const observable = this.securitySvc.login(login, password);

        observable
            .subscribe(data => {
            this.securitySvc.setTokens(data as any);
            this.router.navigate(['dashboard']);

        }, err => { // error
            console.error(err);
        });

        return observable.pipe(finalize( () => {
            this.loginInProgress = false;
        }));
    }

    logOut() {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/x-www-form-urlencoded',
                // will set up automatically via http interceptor
                // 'Authorization': 'Bearer ' + this.securitySvc.token
            })
        };
        this.securitySvc.clearTokens();
        this.router.navigate(['authorization']);
    }

    registration(login: string, password: string) {
        const observable = this.securitySvc.registration(login, password);

        return observable
            .subscribe(data => {
            this.router.navigate(['authorization']);
        }, err => { // error
            console.error(err);
        });

    }
 }
