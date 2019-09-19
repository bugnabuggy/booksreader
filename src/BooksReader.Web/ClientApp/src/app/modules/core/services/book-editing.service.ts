import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CrudService } from './crud.service';
import { Book, BookEditInfo, OperationResult } from '../models';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';

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

  getFull(id: string) {
    return this.http.get<OperationResult<BookEditInfo>>(Endpoints.api.author.bookFullEditInfo.replace('{id}', id)).pipe(share());
  }

  editFull(id:string, bookEditInfo: BookEditInfo) {
    return this.http.put<OperationResult<BookEditInfo>>(Endpoints.api.author.bookFullEditInfo.replace('{id}', id), bookEditInfo).pipe(share());
  }
}
