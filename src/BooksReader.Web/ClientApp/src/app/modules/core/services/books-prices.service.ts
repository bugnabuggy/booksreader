import { Injectable } from '@angular/core';
import { CRUDService } from './crud.service';
import { BookPrice } from '../models';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';

@Injectable({
  providedIn: 'root'
})
export class BooksPricesService extends CRUDService<BookPrice> {

  constructor(
    http: HttpClient
  ) { 
    const baseUrl = Endpoints.api.author.price.replace('/{id}','');
    super(http, baseUrl);
  }

  getByBook(bookId: string) {
    const url = Endpoints.api.author.price.replace('{id}', bookId);

    const observable = this.http.get(url);

    return observable;
  }
}
