
import { Injectable, ApplicationRef } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { SecurityService } from '../services';
import { Observable} from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class SimpleHttpInterceptor implements HttpInterceptor {
  updateInProgress: boolean = false;
  waitingRequests: HttpRequest<any>[] = [];

  constructor(public securitySvc: SecurityService,
    public router: Router,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    debugger;
    if (this.securitySvc.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.securitySvc.getToken()}`
        }
      });
    }
    return next.handle(request).pipe(tap(event => {
      if (event instanceof HttpResponse) {
        // do stuff with response if you want
      }
    }, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          this.securitySvc.clearTokens();
          this.router.navigate(['authorization']);
        }
      }
    }));

  }
}
