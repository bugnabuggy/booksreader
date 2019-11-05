import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BooksFilters, WebResult } from '@br/core/models';
import { Endpoints } from '@br/config';
import { BookMarketDto } from './models/api/dto/public';

@Injectable({
  providedIn: 'root'
})
export class BookMarketService {

  constructor(
    private http: HttpClient
  ) { 
  }

  get(filters: BooksFilters) {
    const url  = Endpoints.api.booksMarket.books;

    var filterHeaders = {};
    Object.keys(filters).forEach(val => filterHeaders[val] = filters[val]);

    const observable = this.http.get<WebResult<BookMarketDto[]>>(url,{ params: filterHeaders});

    return observable;
  }
}
