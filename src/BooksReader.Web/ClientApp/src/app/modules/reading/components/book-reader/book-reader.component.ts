import { Component, OnInit, Input } from '@angular/core';
import { BookChapter, Book } from '@br/core/models';
import { BookReadingService } from '@br/core/services';

@Component({
  selector: 'app-book-reader',
  templateUrl: './book-reader.component.html',
  styleUrls: ['./book-reader.component.scss']
})
export class BookReaderComponent implements OnInit {

  @Input() book: Book;
  @Input() chapters: BookChapter[] = [];

  sidebarIsOpened = false;
  bookChapter: BookChapter;

  constructor (
    private readingSvc: BookReadingService
  ) { }

  ngOnInit() {
    this.readingSvc.activeChapter.subscribe(val=>{
      this.bookChapter = val;
    });
  }

  
}
