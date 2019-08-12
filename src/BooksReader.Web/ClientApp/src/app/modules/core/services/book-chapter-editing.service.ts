import { Injectable } from '@angular/core';
import { CrudService } from './crud.service';
import { BookChapter, BookChapterRequest, OperationResult } from '../models';

import { Endpoints } from '@br/config';
import { HttpClient } from '@angular/common/http';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookChapterEditingService  {

  constructor(
    private http: HttpClient
 ) { }

 createOrUpdate(bookId: string, chpaterRequest: BookChapterRequest){
  const url = Endpoints.api.author.chapter
  .replace('{bookId}', bookId)
  .replace('{id}','');

  const observabel = this.http.post<OperationResult<BookChapter>>(url, chpaterRequest).pipe(share());

  return observabel;
 }


}
