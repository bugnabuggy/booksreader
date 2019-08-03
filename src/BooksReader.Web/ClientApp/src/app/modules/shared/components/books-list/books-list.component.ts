import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Book } from '@br/core/models';
import { BookRenderingType } from '@br/core/enums';
import { BookSelection } from '@br/core/models/site/events';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.scss']
})
export class BooksListComponent implements OnInit {
  @Input() books: Book[];
  @Input() type: BookRenderingType;

  @Output() select = new EventEmitter<BookSelection>()

  BookRenderingType = BookRenderingType;

  constructor() { }

  ngOnInit() {
  }

  selected(selection: BookSelection) {
    this.select.emit(selection);
  }
}
