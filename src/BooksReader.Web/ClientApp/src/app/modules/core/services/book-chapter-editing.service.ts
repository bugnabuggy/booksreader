import { Injectable } from '@angular/core';
import { BehaviorSubject, observable } from 'rxjs';
import { BookChapter, BookChapterRequest, OperationResult, ChapterReorderRequest } from '../models';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';
import { ClearNullValues } from '@br/utilities/clear-null-values';

@Injectable({
  providedIn: 'root'
})
export class BookChapterEditingService {

  readonly activeChapter = new BehaviorSubject<BookChapter>(null);

  constructor(
    private http: HttpClient
  ) {

  }

  save(bookId: string, chpaterRequest: BookChapterRequest) {
    const url = Endpoints.api.author.chapter
      .replace('{bookId}', bookId)
      .replace('{id}', chpaterRequest.id || '');

    chpaterRequest = ClearNullValues(chpaterRequest);
    let observabel = !chpaterRequest.id 
        ? this.http.post<OperationResult<BookChapter>>(url, chpaterRequest)
        : this.http.put<OperationResult<BookChapter>>(url, chpaterRequest)
    
    observabel = observabel.pipe(share());

    return observabel;
  }

  reorder(bookId: string, reorderRequest: ChapterReorderRequest[]) {
    const url = Endpoints.api.author.reorderChapters
      .replace('{bookId}', bookId);

    const observable = this.http.post(url, reorderRequest).pipe(share());

    return observable;
  }

  delete(bookId: string, chapterid: string) {
    const url = Endpoints.api.author.chapter
      .replace('{bookId}', bookId)
      .replace('{id}', chapterid);

      const observable = this.http.delete(url).pipe(share());

      return observable;
  }
}
