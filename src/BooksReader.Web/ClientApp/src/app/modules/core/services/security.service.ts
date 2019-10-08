import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { share, mergeMap, map, tap, finalize } from 'rxjs/operators';

import { from, BehaviorSubject, Observable, Subject, of, Observer, Subscription, timer } from 'rxjs';

import { SiteConstants, Endpoints } from '@br/config';

import { StorageService } from './storage.service';
import { AppUser, AuthResponse } from '../models';

@Injectable({
    providedIn: 'root'
})
export class SecurityService {

    constructor(
        public router: Router,
        public http: HttpClient,
        private storage: StorageService
    ) {
      
    }

    loginRequest(body: FormData): Observable<any> {
      const url = Endpoints.api.authorization.login;

      const observable = this.http.post<AuthResponse>(url, body)
          .pipe(
              tap((val: AuthResponse) => {
                debugger;  
                // this.setTokens(val);
              }),
              // mergeMap(() => {
              //     return this.init();
              // }),
              // mergeMap(() => {
              //     return this.addLoginHistory();
              // }),
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
}
