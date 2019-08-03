import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CrudService } from './crud.service';
import { Book } from '../models';
import { Endpoints } from '@br/config';

@Injectable({
  providedIn: 'root'
})
export class BookEditingService extends CrudService<Book> {

  constructor(
     http: HttpClient
  ) {
    const baseUrl = Endpoints.api.author.book.replace('/{id}','');  
    super(http, baseUrl);
  }

  
}
