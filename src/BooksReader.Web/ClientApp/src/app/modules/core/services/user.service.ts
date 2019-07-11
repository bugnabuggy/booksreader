import { share, finalize, flatMap, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';
import { of, BehaviorSubject } from 'rxjs';
import { UserHubService } from '@br/communications/hubs';
import { AppUser, Language, UserRegistration } from '../models';
import { TranslateService } from '@ngx-translate/core';
import { Endpoints } from '@br/config';
import { HttpClient } from '@angular/common/http';
import { MenuSections } from '@br/config/menu-sections';
import { SiteRoles } from '../enums';
import { AuthorProfile } from '../models/api-contracts/author-profile.dto';

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

    menuSections$ = new BehaviorSubject<any>([]);
    
    get user() { return this.securitySvc.user$.getValue(); }

    get isLoggedIn() {
        return this.securitySvc.isAuthenticated;
    }

    init() {
        let observabe = of(null).pipe(
            flatMap(() => this.securitySvc.init()),
            flatMap( (user: AppUser) => {
                // if we have initialized user, bootstrap the ui 
                if(user) {
                    this.initMenu(user);

                    // start signalR
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

    initMenu(user){
        // init menu
        let menuSections = [];
        user.roles.forEach(x => {
            if(MenuSections[x])
            {
                menuSections.push(MenuSections[x]);
            }
        });
        this.menuSections$.next(menuSections);

    }

    refresh() {
        this.securitySvc.getUserInfo().subscribe(user => {
            debugger;
            this.initMenu(user);
        })
    }

    logIn(login: string, password: string, goToProfile?: boolean) {
        const observable = this.securitySvc.login(login, password);
        observable
            .subscribe(data => {
                // start real time communication with server
                this.initMenu(this.user);
                
                this.userHub.init();

                // navigate user depend on role to different pages
                
                if (goToProfile) {
                    this.router.navigateByUrl(Endpoints.forntend.user.profileUrl);
                    return;
                }

                // order by role priorities
                let redirectDictionary = {
                    [SiteRoles.admin]: Endpoints.forntend.admin.dashboardUrl,
                    [SiteRoles.author]: Endpoints.forntend.author.dashboardUrl,
                    [SiteRoles.reader]: Endpoints.forntend.admin.dashboardUrl,
                };
                
                for(let item in this.user.roles){
                    if(redirectDictionary[this.user.roles[item]]){
                        this.router.navigateByUrl(redirectDictionary[this.user.roles[item]]);
                        break;
                    };
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

    authorRequest(authorData: AuthorProfile) {
        const url = Endpoints.api.user.becomeAnAuthor;
        const observabe = this.http.post(url, authorData).pipe(share());
        return observabe;
    }
}
