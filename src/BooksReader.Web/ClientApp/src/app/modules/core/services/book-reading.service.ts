import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BookChapter, Book, OperationResult, BookReadingDto } from '../models';
import { BooksHubService } from '@br/communications/hubs/books-hub.service';
import { finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BookReadingService {
  readonly activeBook = new BehaviorSubject<BookReadingDto>(null);
  readonly activeChapter = new BehaviorSubject<BookChapter>(null);
  bookId: string = '';
  inProgress = false;

  private readonly chaptersContentMap = new Map();


  constructor(
    private booksHub: BooksHubService,
  ) { }

  clearState() {
    this.activeBook.next(null);
    this.activeChapter.next(null);
  }

  getBook(bookId: string) {
    this.bookId =  bookId;
    const observable = this.booksHub.getBook(bookId);

    return observable;
  }

  setActiveChapter(chapterId: string) {
    let cached =  this.chaptersContentMap.get(chapterId);

    if(cached) {
      this.activeChapter.next(cached);
    } else {
      this.inProgress = true;
      this.booksHub.getChapter(chapterId, this.bookId)
      .pipe(
        finalize(() => {
          this.inProgress = false;
        })
      )
      .subscribe(val => {
          this.chaptersContentMap.set(chapterId, val.data);
          this.activeChapter.next(val.data);
      });
    }
  }
  
}
