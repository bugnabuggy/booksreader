import { Injectable } from '@angular/core';
import { CRUDService } from './crud.service';
import { Endpoints } from '@br/config';
import { HttpClient } from '@angular/common/http';
import { PublicPage } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PublicPagesService extends CRUDService<PublicPage> {

  constructor(
    http: HttpClient
  ) { 
    const baseUrl = Endpoints.api.publicPages.pages;
    super(http, baseUrl)
  }
}
