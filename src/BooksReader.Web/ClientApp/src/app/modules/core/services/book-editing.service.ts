import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';
import { WebResult, Book, OperationResult } from '../models';
import { Observable } from 'rxjs';
import { CRUDService } from './crud.service';

@Injectable({
  providedIn: 'root'
})
export class BookEditingService extends CRUDService<Book> {

  constructor(
    http: HttpClient
  ) { 
    const baseUrl = Endpoints.api.author.book.replace('/{id}', '');
    super(http, baseUrl);
  }

  getBooks(filters): Observable<WebResult<Book[]>> {
    filters = filters || {title: '', description: ''};

    const observable = this.http.get<WebResult<Book[]>>(this.baseUrl, {
      params: filters
    }).pipe(share());

    return observable;
  }

  getFull(bookId: string) {
    const url = Endpoints.api.author.bookFullEditInfo.replace('{id}', bookId);
    const observable = this.http.get(url).pipe(share());
    return observable;
  }

  editFull(book: Book) {
    const url = Endpoints.api.author.bookFullEditInfo.replace('{id}', book.id);
    const observable = this.http.put<OperationResult<Book>>(url, book).pipe(share());
    return observable;

  }

}

