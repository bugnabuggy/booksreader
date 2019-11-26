import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReaderDashboardDto, Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';

@Component({
  selector: 'app-reader-books-list-item',
  templateUrl: './reader-books-list-item.component.html',
  styleUrls: ['./reader-books-list-item.component.scss']
})
export class ReaderBooksListItemComponent implements OnInit {
  @Input() bookSubscription: ReaderDashboardDto;

  @Output() action = new EventEmitter<Action<ReaderDashboardDto>>();

  constructor() { 

  }

  ngOnInit() {
  }

  read() {
    this.action.emit({
      type: ActionType.select,
      data: this.bookSubscription
    } as Action<ReaderDashboardDto>);
  }

  remove() {
    this.action.emit({
      type: ActionType.delete,
      data: this.bookSubscription
    } as Action<ReaderDashboardDto>);
  }

}
