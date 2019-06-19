import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookEditService {

  constructor(
    private http: HttpClient
  ) { }

  getBook(id: string) {
    const url = Endpoints.api.author.book.replace('{id}', id);
    const observable = this.http.get(url).pipe(share());

    return observable;
  }

  getChapters(bookId: string) {
    const url = Endpoints.api.author.chapter
      .replace('{bookId}', bookId)
      .replace('{id}', '');
    const observable = this.http.get(url).pipe(share());

    return observable;
  }
}
