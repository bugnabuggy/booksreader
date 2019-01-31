import { Component, OnInit } from '@angular/core';
import { Book, WebResult, OperationResult } from '../../../models';
import { ReaderBooksService } from '../../../services';

@Component({
  selector: 'app-book-market',
  templateUrl: './book-market.component.html',
  styleUrls: ['./book-market.component.scss']
})
export class BookMarketComponent implements OnInit {
  books: Book[];

  constructor(
    private readerBooksService: ReaderBooksService
  ) { }

  ngOnInit() {
    this.readerBooksService.getReaderBooks()
    .subscribe( (val: OperationResult<Book[]>) => {
      if ( val.success ) {
        this.books = val.data;
      }
    });
  }

}
