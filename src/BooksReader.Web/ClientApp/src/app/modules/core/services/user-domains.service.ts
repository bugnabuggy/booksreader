import { Injectable } from '@angular/core';
import { CRUDService } from './crud.service';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { UserDomain, UserDomainState, OperationResult } from '../models';
import { share } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserDomainsService extends CRUDService<UserDomain> {

  constructor(
    http: HttpClient
  ) { 
    const baseUrl = Endpoints.api.domains.domain;
    super(http, baseUrl)
  }

  toggleState(domainState: UserDomainState) {
    const url = Endpoints.api.domains.toggleState.replace('{id}', domainState.domainId);
    const observable = this.http.put<OperationResult<UserDomain>>(url, {}).pipe(share());

    return observable;
  }
}
