import { Injectable } from '@angular/core';
import { CRUDService } from './crud.service';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { UserDomain } from '../models';


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
}
