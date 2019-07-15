import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PublicPageInfo, PublicPageInfoRequest } from '../models';
import { Observable } from 'rxjs';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class PublicService {

  constructor(
    private http: HttpClient,
  ) { }

  getPageInfo(): Observable<PublicPageInfo> {    
    const url = Endpoints.api.public.pageInfo;
    const request  = {
      domain: location.host,
      urlPath: location.pathname
    } as PublicPageInfoRequest;

    const httpOptions = {
      headers: new HttpHeaders({
        'domain': request.domain,
        'urlPath': request.urlPath,
        'promoCode': request.promoCode || ''
      })
    }

    const observabel = this.http.get<PublicPageInfo>(url, httpOptions).pipe(share());

    return observabel;
  }
}
