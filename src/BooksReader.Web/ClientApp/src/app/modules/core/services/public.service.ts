import { Injectable, Optional, Inject } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PublicPageInfo, PublicPageInfoRequest } from '../models';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PublicService {

  constructor(
    private http: HttpClient,
    private location: Location,
    @Optional() @Inject('BASE_URL') public baseUrl: string,
    @Optional() @Inject('HOST_NAME') public host: string,
    @Optional() @Inject('SERV_ENV') public env: string 
  ) { }

  getPageInfo(): Observable<PublicPageInfo> {    
    const url = Endpoints.api.public.pageInfo;
    
    let host = this.host || location.host;
    let path = this.location.path(true);

    const request  = {
      domain: host,
      urlPath: path
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
