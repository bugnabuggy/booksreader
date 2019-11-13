import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BooksFilters, WebResult, BookSubscription, OperationResult } from '@br/core/models';
import { Endpoints } from '@br/config';
import { BookMarketDto } from '../models/api/dto/public';
import { share } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookMarketService {

  books = new Map();

  constructor(
    private http: HttpClient
  ) { 
  }

  list(filters: BooksFilters) {
    const url  = Endpoints.api.booksMarket.books;

    var filterHeaders = {};
    Object.keys(filters).forEach(val => filterHeaders[val] = filters[val]);

    const observable = this.http.get<WebResult<BookMarketDto[]>>(url,{ params: filterHeaders})
      .pipe(share());
      
      this.books.clear();

      observable.subscribe(x=> {
        for(let book of x.data) {
          this.books.set(book.bookId, book);
        }
      })

    return observable;
  }

  get(bookId: string) {
    let book = this.books.get(bookId);
    if(book) {
      return of(book);
    }

    const url = Endpoints.api.booksMarket.books + '/'+ bookId;

    const observable = this.http.get<BookMarketDto>(url);

    return observable;
  }

  add(book: BookMarketDto) {
    const url = Endpoints.api.booksMarket.books + `/${book.bookId}/add` ;
    const observable = this.http.post<OperationResult<BookSubscription>>(url,{});
    return observable;
  }

  buy(book: BookMarketDto) {

      return of(null);
  }

}
