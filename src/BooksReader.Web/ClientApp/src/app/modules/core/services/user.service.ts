import { Injectable } from '@angular/core';
import { of, BehaviorSubject } from 'rxjs';
import { share, flatMap } from 'rxjs/operators';
import { NotificationService } from './notification.service';
import { AppUser, RegistrationRequest, Language, AuthorRequest, LogoutData } from '../models';
import { SecurityService } from './security.service';
import { MenuSections } from '@br/config/menu-sections';
import { SiteRoles } from '../enums';
import { Endpoints, SiteConstants } from '@br/config';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UserHubService } from '../../communications/hubs/user-hub.service';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _isUiVisible = true;
  private _keepUi = false;
  private _lang = '';

  menuSections$ = new BehaviorSubject<any>([]);
  logoutData: LogoutData = null;

  constructor(
    private notifications: NotificationService,
    private securitySvc: SecurityService,
    private storage: StorageService,
    private http: HttpClient,
    public router: Router,
    public translate: TranslateService,
    private userHub: UserHubService
  ) { 
    this._keepUi = JSON.parse(this.storage.getItem(SiteConstants.storageKeys.uiIsShown));
  }

  get user(): AppUser { return this.securitySvc.user$.getValue(); }

  get authorized() {
    return this.securitySvc.isAuthorized;
  }

  get keepUi() {
     return this._keepUi;
  }

  get isUiVisible() {
    return this._isUiVisible;
  }

  get language() {
    return this._lang;
  }

  public toggleUi(isVisible) {
    this._isUiVisible = isVisible;

    if(isVisible) {
      this.storage.setItem(SiteConstants.storageKeys.uiIsShown, isVisible);
    }
  }

  init() {
    this._lang = this.storage.getItem(SiteConstants.storageKeys.userLang) || SiteConstants.defaultLanguage;
    
    this.translate.setDefaultLang(this._lang);

    let observabe = of(null).pipe(
      flatMap(() => this.securitySvc.init()),
      flatMap((user: AppUser) => {
        // if we have initialized user, bootstrap the ui 
        if (user) {
          this.bootstrap(user);
        }
        return of(null);

      }),
      share()
    );

    observabe.subscribe(val => {
      console.warn('the app initialized');
    }, err => {
      this.notifications.showError(err);
    })

    return observabe;
  }

  private bootstrap(user: AppUser) {
    this.initMenu(user);
    // start signalR
    return this.userHub.init(this.logOut.bind(this))
  }

  initMenu(user) {
        let menuSections = [];
    user.roles.forEach(x => {
      if (MenuSections[x]) {
        menuSections.push(MenuSections[x]);
      }
    });
    this.menuSections$.next(menuSections);
  }

  refresh() {
    this.securitySvc.getUserInfo().subscribe(user => {
      this.initMenu(user);
    })
  }

  logIn(login: string, password: string, goToProfile?: boolean) {
    const observable = this.securitySvc.login(login, password);
    observable
      .subscribe(data => {
        // set up infrastructure
        this.bootstrap(this.user);

        // navigate user depend on role to different pages
        if (goToProfile) {
          this.router.navigateByUrl(Endpoints.frontend.user.profileUrl);
          return;
        }

        // order by role priorities
        let redirectDictionary = {
          [SiteRoles.admin]: Endpoints.frontend.admin.dashboardUrl,
          [SiteRoles.author]: Endpoints.frontend.author.dashboardUrl,
          [SiteRoles.reader]: Endpoints.frontend.reader.dashboardUrl,
        };

        for (let item in this.user.roles) {
          if (redirectDictionary[this.user.roles[item]]) {
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

  logOut(logoutData?: LogoutData) {
    this.userHub.stop();
    this.securitySvc.logout();
    this.menuSections$.next([]);

    if(logoutData) {
      this.logoutData =  logoutData;
      this.router.navigateByUrl(Endpoints.frontend.user.forceLogoutUrl);
    }
    else {
      this.router.navigate([Endpoints.frontend.main]);
    }
    
  }

  registration(model: RegistrationRequest) {
    let observable = this.securitySvc.register(model).pipe(
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
    this.storage.setItem(SiteConstants.storageKeys.userLang, lang.code);
    this.translate.use(lang.code);
  }

  updateProfile(profile: AppUser) {
    const url = Endpoints.api.user.profile;
    const observable = this.http.put<AppUser>(url, profile).pipe(share());
    return observable;
  }

  authorRequest(authorData: AuthorRequest) {
    const url = Endpoints.api.user.becomeAnAuthor;
    const observabe = this.http.post(url, authorData).pipe(share());
    return observabe;
  }

  get isAuthor() {
    return this.securitySvc.isInRole(SiteRoles.author)
  }

  get isAdmin() {
    return this.securitySvc.isInRole(SiteRoles.admin)
  }
}
