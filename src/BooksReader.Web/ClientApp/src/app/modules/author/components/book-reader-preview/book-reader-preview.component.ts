import { Component, OnInit, Input } from '@angular/core';
import { Book, BookChapter } from '@br/core/models';
import { BookChapterEditingService } from '@br/core/services';

@Component({
  selector: 'app-book-reader-preview',
  templateUrl: './book-reader-preview.component.html',
  styleUrls: ['./book-reader-preview.component.scss']
})
export class BookReaderPreviewComponent implements OnInit {

  @Input() book: Book;

  bookChapter: BookChapter;

  constructor(
    private chapterEditingSvc: BookChapterEditingService   
  ) { }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe(val=>{
      this.bookChapter = val;
    });
  }

}
