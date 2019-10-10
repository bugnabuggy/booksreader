import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { share, mergeMap, map, tap, finalize } from 'rxjs/operators';

import { from, BehaviorSubject, Observable, Subject, of, Observer, Subscription, timer } from 'rxjs';

import { SiteConstants, Endpoints } from '@br/config';

import { StorageService } from './storage.service';
import { AppUser, AuthResponse, RegistrationRequest, ChangePasswordRquest, LoginHistoryRequest, LogoutData } from '../models';
import { copyObject } from '@br/utilities/copy-object';

@Injectable({
    providedIn: 'root'
})
export class SecurityService {

    private access_token = '';
    private tokenExpirationDate: Date;
    private isLoggedIn: boolean = false;
    private isLoggedIn$: Observable<boolean>;

    // Show whether all actions to determin whether user logged in or not have been done
    private isInitialized: boolean = false;
    private initRequest$: Observable<any>;

    readonly user$ = new BehaviorSubject<AppUser>(null);

    constructor(
        public router: Router,
        public http: HttpClient,
        private storage: StorageService
    ) {
        const tokens = storage.getItem(SiteConstants.storageKeys.userToken) || null;
        const userTokens = JSON.parse(tokens);
        if (userTokens) {
            this.setTokens(userTokens);
        }

        this.isLoggedIn$ = Observable.create((observer: Observer<boolean>) => {
            if (this.isInitialized) {
                observer.next(this.isLoggedIn);
                observer.complete();
                return;
            }

            // otherwise start initialization process 
            let observable = this.initRequest$;
            if(!this.initRequest$) {
                observable = this.init();
            }

            observable
                .pipe(finalize(() => {
                    this.isInitialized = true;
                    observer.next(this.isLoggedIn);
                    observer.complete();
                }))
                .subscribe();
        })
    }

    get token(): string {
        return this.access_token;
    }

    get isAuthorized(): boolean
    {
        return this.isLoggedIn;
    }

    get auth$ () {
        return this.isLoggedIn$;
    }

    init() {
        if (this.access_token) {
            this.initRequest$ = this.getUserInfo();

            this.initRequest$.subscribe((user) => {
                this.isLoggedIn = true;    
            },
            (err) => {
                if (err.status == 401 )  {
                    this.isLoggedIn = false;
                }

                // in case no user found 
                if (err.status == 404) {
                    this.clearTokens();
                }
                // else probably retry or do some kind of disconected app    
            });

            return this.initRequest$;
        }

        this.isLoggedIn = false;
        return of(null);
    }

    getUserInfo(): Observable<AppUser> {
        const url = Endpoints.api.user.info;
        const observable = this.http.get<AppUser>(url).pipe(share());

        observable.subscribe((val: AppUser) => {
            this.user$.next(val);
        });

        return observable;
    }

    register(model: RegistrationRequest){
        const url = Endpoints.api.authorization.registration;
        const observable = this.http.post(url, model).pipe(share());
        return observable;
    }


    protected loginRequest(body: FormData): Observable<any> {
      const url = Endpoints.api.authorization.login;

      const observable = this.http.post<AuthResponse>(url, body)
          .pipe(
              tap((val: AuthResponse) => {
                this.setTokens(val);
              }),
              mergeMap(() => {
                  return this.init();
              }),
              mergeMap(() => {
                  return this.addLoginHistory();
              }),
              share(),
          );

      return observable;
  }

    login(login, password) {
        const body = new FormData();
        body.append('client_id', 'mvc');
        body.append('client_secret', 'secret');
        body.append('grant_type', 'password');
        body.append('username', login);
        body.append('password', password);

        return this.loginRequest(body);
    }

    externalLogin(type: string, access_token) {
        const body = new FormData();
        body.append('client_id', 'mvc');
        body.append('client_secret', 'secret');
        body.append('grant_type', 'external');
        body.append('provider', type);
        body.append('external_token', access_token);

        return this.loginRequest(body);
    }

    sendLoggingData(loginHistory: LoginHistoryRequest) {
        const url = Endpoints.api.user.loginHistory;
        return this.http.post(url, loginHistory).pipe(share());
    }

    addLoginHistory() {
        const loginHistory = {
            userAgent: navigator.userAgent,
            screen: copyObject(window.screen),
        } as LoginHistoryRequest;

        let timeoutId: Subscription;

        const promise = new Promise((resolve, reject) => {
            
            navigator.geolocation.getCurrentPosition((val) => {
                loginHistory.coordinates = copyObject(val.coords);
                if (timeoutId) {
                    timeoutId.unsubscribe();
                    timeoutId = null;
                }
                this.sendLoggingData(loginHistory)
                    .subscribe(resp => {
                        resolve(resp);
                    }, (err) => {
                        reject(err);
                    });
            });

            timeoutId = timer(SiteConstants.Short_timeout).subscribe(() => {
                this.sendLoggingData(loginHistory)
                    .subscribe(resp => {
                        resolve(resp);
                        timeoutId.unsubscribe();
                        timeoutId = null;
                    }, (err) => {
                        reject(err);
                    });
            });
        });

        // this will be bug ↓ because link to an object would change
        return from(promise);
    }

    setTokens(authResponse: AuthResponse) {
        this.access_token = authResponse.access_token;

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
        this.access_token = null;
        this.tokenExpirationDate = null;
        this.storage.removeItem(SiteConstants.storageKeys.userToken);
    }

    logout() {
        this.clearTokens();
        this.isLoggedIn = false;
    }

    isInRole(role: string) {
        try {
            let user = this.user$.getValue();
            return user.roles.findIndex(x => x === role) > -1;
        } catch (exp) {
            return false;
        }
    }

    changePassword(model: ChangePasswordRquest): Observable<any> {
        const url = Endpoints.api.authorization.changePassword;
        return this.http.post(url, model).pipe(share());
    }
}
