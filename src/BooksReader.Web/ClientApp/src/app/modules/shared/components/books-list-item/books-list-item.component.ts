import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Book } from '@br/core/models';
import { BookSelection } from '@br/core/models/site/events';

@Component({
  selector: 'app-books-list-item',
  templateUrl: './books-list-item.component.html',
  styleUrls: ['./books-list-item.component.scss']
})
export class BooksListItemComponent implements OnInit {

  @Input() book: Book;
  @Output() select = new EventEmitter<BookSelection>()

  constructor() { }

  ngOnInit() {
  }

  
}
