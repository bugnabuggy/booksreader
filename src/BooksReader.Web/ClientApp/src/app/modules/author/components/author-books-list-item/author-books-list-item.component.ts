import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Book, Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';

@Component({
  selector: 'app-author-books-list-item',
  templateUrl: './author-books-list-item.component.html',
  styleUrls: ['./author-books-list-item.component.scss']
})
export class AuthorBooksListItemComponent implements OnInit {

  @Input() book: Book;
  @Output() action = new EventEmitter<Action<Book>>();

  constructor(
  ) { }

  ngOnInit() {
  }

  fastEdit() {
    this.action.emit({
      data: this.book,
      type: ActionType.fastEdit
    });
  }

  edit() {
    this.action.emit({
      data: this.book,
      type: ActionType.edit
    });
  }

  delete() {
    this.action.emit({
      data: this.book,
      type: ActionType.delete
    });
  }

}
