import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Action, Book, OperationResult, ConfirmationDialogModel } from '@br/core/models';
import { MatDialog } from '@angular/material/dialog';
import { BookEditingService, NotificationService } from '@br/core/services';
import { AddBookDialogComponent } from '../add-book-dialog/add-book-dialog.component';
import { SiteMessages } from '@br/config/site-messages';
import { ActionType, ConfirmationType, ConfirmationResult } from '@br/core/enums';
import { Router } from '@angular/router';
import { Endpoints } from '@br/config';
import { ClearNullValues } from '@br/utilities/clear-null-values';
import { ConfirmationDialogComponent } from '@br/controls/dialogs';

@Component({
  selector: 'app-author-books-list',
  templateUrl: './author-books-list.component.html',
  styleUrls: ['./author-books-list.component.scss']
})
export class AuthorBooksListComponent implements OnInit {

  @Input() books = [];

  actions = {};

  constructor(
    public dialog: MatDialog,
    private bookSvc: BookEditingService,
    private notifications: NotificationService,
    private router: Router
  ) {
    this.actions[ActionType.fastEdit] = this.rename;
    this.actions[ActionType.delete] = this.delete;
    this.actions[ActionType.edit] = this.edit;
  }

  ngOnInit() {
  }

  doAction(action: Action<Book>) {
    let actionFunc = this.actions[action.action];

    if (actionFunc) {
      actionFunc = actionFunc.bind(this);
      actionFunc(action.data);
    }
  }

  add() {
    const dialogRef = this.dialog.open(AddBookDialogComponent, {
      minHeight: "50%",
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        result = ClearNullValues(result);
        this.bookSvc.add(result)
          .subscribe((val: OperationResult<Book>) => {
            debugger;
            this.books.push(val.data);
            this.notifications.showSuccess(SiteMessages.author.books.added);
          });
      }
    });
  }

  edit(book: Book) {
    book = ClearNullValues(book);

    this.router.navigateByUrl(Endpoints.frontend.author.bookUrl
      .replace(':id', book.id)
      .replace(':tab', ''));
  }

  rename(book: Book) {
    const dialogRef = this.dialog.open(AddBookDialogComponent, {
      minHeight: "50%",
      data: book
    });

    dialogRef.afterClosed().subscribe(bookBasicInfo => {
      if (bookBasicInfo) {
        this.bookSvc.update(bookBasicInfo)
          .subscribe((val: OperationResult<Book>) => {
            let index = this.books.findIndex(x => x.id == bookBasicInfo.id);
            this.books[index] = val.data;

            this.notifications.showSuccess(SiteMessages.author.books.edited)
          });
      }
    });
  }

  delete(book: Book) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        type: ConfirmationType.yesNo,
        text: SiteMessages.author.books.deleteQuestion
      } as ConfirmationDialogModel
    });

    dialogRef.afterClosed().subscribe(confirmresult  => {
      if (confirmresult == ConfirmationResult.yes) {
        this.bookSvc.delete(book.id)
          .subscribe((val: OperationResult<Book>) => {
            let index = this.books.findIndex(x => x.id == book.id);
            this.books.splice(index, 1);

            this.notifications.showSuccess(SiteMessages.author.books.deleted)
          });
      }
    });
  }
}
