import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router, CanActivate } from '@angular/router';
import { share, mergeMap } from 'rxjs/operators';
import { Endpoints } from '../enums/Endpoints';
import { AuthResponse } from '../api/contracts/auth-response.contract';
import { AppUser } from '../models';

@Injectable()
export class SecurityService {
    isLoggedIn: boolean = true;
    token: string;
    isAdmin: boolean = false;
    tokenExpirationDate: Date;

    user = {} as  AppUser;

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
            .subscribe((val: AppUser) => {
                this.user = val;
                this.router.navigate(['dashboard']);
            },
            (err) => {
            });
        }
    }

    canActivate(): boolean {
        if (!this.isAuthenticated()) {
            this.router.navigate(['authorization']);
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
            httpOptions).pipe(share());

        return observable;
    }

    logout() {
        this.clearTokens();
        this.router.navigate(['authorization']);
    }

    registration(username: string, password: string) {
        const antiForgeryUrl = Endpoints.api.authorization.antiforgery ;

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

    getUserInfo() {
        const url = Endpoints.api.user.info;
        return this.http.get(url).pipe(share());
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
