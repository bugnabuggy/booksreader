import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { BookChapter, Book } from '@br/core/models';
import { BookReadingService } from '@br/core/services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-book-reader',
  templateUrl: './book-reader.component.html',
  styleUrls: ['./book-reader.component.scss']
})
export class BookReaderComponent implements OnInit, OnDestroy {

  @Input() book: Book;
  @Input() chapters: BookChapter[] = [];

  sidebarIsOpened = false;
  bookChapter: BookChapter;

  activeChapterSub: Subscription;

  constructor (
    private readingSvc: BookReadingService
  ) { }

  ngOnInit() {
    this.activeChapterSub = this.readingSvc.activeChapter.subscribe(val => {
      this.bookChapter = val;
    });
  }

  ngOnDestroy() {
    this.activeChapterSub.unsubscribe();
  }
}
