import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router, CanActivate } from '@angular/router';
import { share, mergeMap, map } from 'rxjs/operators';
import { Endpoints } from '../enums/Endpoints';
import { AppUser, LoginHistoryModel } from '../models';
import { LogoutData, AuthResponse } from '../models/api-contracts';
import { Observable, from } from 'rxjs';
import { SiteConstants } from '../enums';
import { flatten } from '../utilities/helpers';
import { StandardFilters } from '../models/filters';

@Injectable()
export class SecurityService {
    isLoggedIn: boolean = true;
    token: string;
    tokenExpirationDate: Date;
    logoutData: LogoutData;
    user = {} as AppUser;

    constructor(
        public router: Router,
        public http: HttpClient
    ) {
        const userTokens = JSON.parse(localStorage.getItem('adminTokens'));
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
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/x-www-form-urlencoded',
            })
        };
        const body = new HttpParams()
            .set('client_id', 'mvc')
            .set('client_secret', 'secret')
            .set('grant_type', 'password')
            .set('username', login)
            .set('password', password);

        const url = Endpoints.api.authorization.login;

        const observable = this.http.post(url,
            body,
            httpOptions).pipe(
                share(),
                map((val: AuthResponse) => {
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

        localStorage.setItem('adminTokens', JSON.stringify(authResponse));
    }

    clearTokens() {
        this.token = null;

        this.tokenExpirationDate = null;
        localStorage.removeItem('adminTokens');
    }

    getToken(): string {
        return this.token;
    }

    isAuthenticated() {
        return !!this.token;
    }
}
