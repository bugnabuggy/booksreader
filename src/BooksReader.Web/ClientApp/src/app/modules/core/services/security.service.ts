import { Injectable } from '@angular/core';
import { HttpClient,  HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { share, mergeMap, map } from 'rxjs/operators';
import { Endpoints } from '../../../config/endpoints';

import { AppUser, LoginHistoryModel } from '@br/core/models';
import { LogoutData, AuthResponse } from '../models/api-contracts';

import { from } from 'rxjs';

import { SiteConstants } from '@br/config';

import { flatten } from '@br/utilities/helpers';
import { StandardFilters } from '../models/filters';
import { StorageService } from './storage.service';

@Injectable({
    providedIn: 'root'
})
export class SecurityService {
    isLoggedIn: boolean = true;
    token: string;
    tokenExpirationDate: Date;
    logoutData: LogoutData;
    user: AppUser = null;

    constructor(
        public router: Router,
        public http: HttpClient,
        private storage: StorageService 
    ) {
        const userTokens = JSON.parse(storage.getItem(SiteConstants.storageKeys.userToken));
        if (userTokens) {
            this.setTokens(userTokens);
        }
    }

    init() {
        if (this.token) {
            this.getUserInfo()
                .pipe(share())
                .subscribe(() => {
                    // this.router.navigate(['dashboard']);
                },
                    (err) => {
                    });
        }
    }

    canActivate(): boolean {
        if (!this.isAuthenticated()) {
            this.router.navigate(['authorize']);
            this.isLoggedIn = false;
            return false;
        } else {
            this.isLoggedIn = true;
            return true;
        }
    }

    login(login, password) {
        const body = new FormData();
        body.append('client_id', 'mvc');
        body.append('client_secret', 'secret');
        body.append('grant_type', 'password');
        body.append('username', login);
        body.append('password', password);

        const url = Endpoints.api.authorization.login;

        const observable = this.http.post(url,
            body).pipe(
                share(),
                map((val: AuthResponse) => {
                    this.setTokens(val);
                }),
                mergeMap(() => this.getUserInfo()),
                mergeMap(() => this.addLoginHistory()));

        return observable;
    }

    externalLogin(type: string, access_token) {
        const url = Endpoints.api.authorization.login;

        const body = new FormData();
        body.append('client_id', 'mvc');
        body.append('client_secret', 'secret');
        body.append('grant_type', 'external');
        body.append('provider', type);
        body.append('external_token', access_token);

        const observable = this.http.post(url,
            body).pipe(
                share(),
                map((val: AuthResponse) => {
                    debugger;
                    this.setTokens(val);
                }),
                mergeMap(() => this.getUserInfo()),
                mergeMap(() => this.addLoginHistory()));

        return observable;
    }

    logout(isForce: boolean) {
        this.clearTokens();
        if (isForce) {
            this.router.navigate(['logout']);
        } else {
            this.router.navigate(['authorize']);
        }
    }

    registration(username: string, password: string) {
        const antiForgeryUrl = Endpoints.api.authorization.antiforgery;

        const observable = this.http.get(antiForgeryUrl)
            .pipe(
                share(),
                mergeMap((val: any) => {
                    return this.http.post(Endpoints.api.authorization.registration, {
                        antiforgeryKey: val,
                        username,
                        password
                    });
                })
            );

        return observable;
    }

    sendLoggingData(loginHistory: LoginHistoryModel ) {
        const url = Endpoints.api.user.loginHistory;
        return this.http.post(url, loginHistory).pipe(share());
    }

    addLoginHistory() {
        const loginHistory = {
            userAgent: navigator.userAgent,
            screen: flatten(window.screen),
        } as LoginHistoryModel;

        let timeoutId;

        const promise = new Promise((resolve, reject) => {
            navigator.geolocation.getCurrentPosition((val) => {
                loginHistory.coordinates = flatten(val.coords);
                if ( timeoutId > 0) {
                    clearTimeout(timeoutId);
                    this.sendLoggingData(loginHistory)
                    .subscribe( resp => {
                        resolve(resp);
                    }, (err) => {
                        reject(err);
                    });
                }
            });

            timeoutId = setTimeout(() => {
                clearTimeout(timeoutId);
                timeoutId = 0;
                this.sendLoggingData(loginHistory)
                .subscribe( resp => {
                    resolve(resp);
                }, (err) => {
                    reject(err);
                });
            }, SiteConstants.Short_timeout);
        });

        // this will be bug ↓ because link to an object would change
        return from(promise);
    }
    
    getLogHistory(filters?: StandardFilters) {
        filters.isDesc = typeof filters.isDesc === 'undefined' ? null : filters.isDesc;
        const  params = filters
            ? new HttpParams()
                .set('OrderByField', filters.orderByField || '')
                .set('IsDesc', <string><any>filters.isDesc)
                .set('PageSize', <string><any>filters.pageSize || '')
                .set('PageNumber', <string><any>filters.pageNumber || '')
            : null;
            const url = Endpoints.api.user.loginHistory;
            const observable = this.http.get(url,
                { params }).pipe(share());
            return observable;
    }

    getUserInfo() {
        const url = Endpoints.api.user.info;
        const observable = this.http.get(url).pipe(share());
        observable.subscribe((val: AppUser) => {
            this.user = val;
        });
        return observable;
    }

    isInRole(role: string) {
        try {
            return this.user.roles.findIndex(x => x === role) > -1;
        } catch (exp) {
            return false;
        }
    }

    get isAdmin(): boolean {
        return this.isInRole('Admin');
    }

    get isAuthor(): boolean {
        return this.isInRole('Author');
    }


    setTokens(authResponse: AuthResponse) {
        this.token = authResponse.access_token;

        if (authResponse.tokenExpirationDate) {
            this.tokenExpirationDate = authResponse.tokenExpirationDate;
        } else {
            this.tokenExpirationDate = new Date();
            this.tokenExpirationDate.setSeconds(authResponse.expires_in);
            authResponse.tokenExpirationDate = this.tokenExpirationDate;
            delete authResponse['expires_in'];
        }

        this.storage.setItem(SiteConstants.storageKeys.userToken, JSON.stringify(authResponse));
    }

    clearTokens() {
        this.token = null;
        this.user = null;

        this.tokenExpirationDate = null;
        this.storage.removeItem(SiteConstants.storageKeys.userToken);
    }

    getToken(): string {
        return this.token;
    }

    isAuthenticated() {
        return !!this.token;
    }
}
