import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BookMarketDto } from '@br/core/models/api/dto/public';
import { Endpoints } from '@br/config';
import { Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';

@Component({
  selector: 'app-books-list-item',
  templateUrl: './books-list-item.component.html',
  styleUrls: ['./books-list-item.component.scss']
})
export class BooksListItemComponent implements OnInit {
  @Input() book: BookMarketDto;
  @Output() action = new EventEmitter<Action<BookMarketDto>>();

  Urls = Endpoints.frontend;
  
  constructor() { }

  ngOnInit() {
  }

  add() {
    this.action.emit({
      action: ActionType.add,
      data: this.book
    });
  }
  
  buy() {
    this.action.emit({
      action: ActionType.select,
      data: this.book
    });
  }
}
