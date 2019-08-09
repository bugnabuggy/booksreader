import { Component, OnInit, Input } from '@angular/core';
import { Book, BookChapter } from '@br/core/models';

@Component({
  selector: 'app-book-content-editor',
  templateUrl: './book-content-editor.component.html',
  styleUrls: ['./book-content-editor.component.scss']
})
export class BookContentEditorComponent implements OnInit {

  @Input() book: Book;
  @Input() chapters: BookChapter[];

  constructor() { }

  ngOnInit() {
  }

}
