import { Injectable } from '@angular/core';
import {  SecurityService } from '../services';

import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  CanLoad,
  Route,
  UrlSegment
} from '@angular/router';

import { Endpoints } from '@br/config';
import { Observable, of } from 'rxjs';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanLoad {

  constructor(
    public security: SecurityService,
    public router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const guard = this.security.auth$.pipe(share());
    
    guard.subscribe((val) => {
      if (!val) {
        this.router.navigate([Endpoints.frontend.authorization]);
      }
      return val;
    }, 
    err => {
        this.router.navigate([Endpoints.frontend.error]);
    });

    return guard;
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    return this.canActivate(null, null);
  }
}
