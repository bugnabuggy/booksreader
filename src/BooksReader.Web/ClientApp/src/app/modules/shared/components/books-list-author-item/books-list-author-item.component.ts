import { Component, OnInit } from '@angular/core';

import { BookSelection } from '@br/core/models/site/events';
import { BookSelectionType } from '@br/core/enums';
import { BooksListItemComponent } from '../books-list-item/books-list-item.component';

@Component({
  selector: 'app-books-list-author-item',
  templateUrl: './books-list-author-item.component.html',
  styleUrls: ['./books-list-author-item.component.scss']
})
export class BooksListAuthorItemComponent extends BooksListItemComponent implements OnInit {

  ngOnInit() {
  }

  fastEdit() {
    this.select.emit({
      book: this.book,
      event: BookSelectionType.select
    } as BookSelection);
  }

  edit() {
    this.select.emit({
      book: this.book,
      event: BookSelectionType.edit
    } as BookSelection);
  }

  delete() {
    this.select.emit({
      book: this.book,
      event: BookSelectionType.delete
    } as BookSelection);
  }
}
