import { Injectable } from '@angular/core';
import { CrudService } from './crud.service';
import { BookChapter, BookChapterRequest, OperationResult, ChapterReorderRequest } from '../models';

import { Endpoints } from '@br/config';
import { HttpClient } from '@angular/common/http';
import { share } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookChapterEditingService  {
  readonly activeChapter = new BehaviorSubject<BookChapter>(null);
  

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

 reorder(bookId:string ,order: ChapterReorderRequest[]) {
  const url = Endpoints.api.author.chapter
  .replace('{bookId}', bookId)
  .replace('{id}','reorder');

  const observable = this.http.post<OperationResult<any>>(url, order).pipe(share());
  return observable;
 }

 delete(bookId: string, chapterId: string) {
  const url = Endpoints.api.author.chapter
  .replace('{bookId}', bookId)
  .replace('{id}', chapterId);
  
  const observabel = this.http.delete(url).pipe(share());
  return observabel;
 }
}
