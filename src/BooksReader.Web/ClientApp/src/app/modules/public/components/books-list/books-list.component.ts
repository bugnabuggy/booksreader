import { Component, OnInit, Input } from '@angular/core';
import { BookMarketDto } from '@br/core/models/api/dto/public';
import { BookMarketService, NotificationService, UserService } from '@br/core/services';
import { ActionType, ConfirmationType, ConfirmationResult } from '@br/core/enums';
import { Action, ConfirmationDialogModel, OperationResult, Book } from '@br/core/models';
import { SiteMessages } from '@br/config/site-messages';
import { SubscriptionStatus } from '@br/core/enums/subscription-status';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '@br/controls/dialogs';
import { LoginOrRegisterComponent } from '../login-or-register/login-or-register.component';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.scss']
})
export class BooksListComponent implements OnInit {
  @Input() books: BookMarketDto[] = [];

  private actions = {};

  constructor(
    private bookMarketSvc: BookMarketService,
    private notifications: NotificationService,
    private userSvc: UserService,
    private dialog: MatDialog,
  ) {
    this.actions[ActionType.add] = this.add.bind(this);
    this.actions[ActionType.select] = this.buy.bind(this);
  }

  ngOnInit() {
  }

  doAction(event: Action<BookMarketDto>) {
    let func = this.actions[event.type];
    if( !this.userSvc.authorized) {
      const dialogRef = this.dialog.open(LoginOrRegisterComponent, {
        minHeight: "50%",
      });

      return;
    }

    if (func) {
      func(event.data);
    }
  }

  add(book: BookMarketDto) {
    this.bookMarketSvc.add(book)
    .subscribe(x => {
        this.notifications.showSuccess(SiteMessages.booksMarket.bookAdded);
        this.books.find(y => y.bookId == x.data.bookId).subscription = SubscriptionStatus.active;
    }, err=>{
      this.notifications.showError(err);
    });
  }

  buy(book: BookMarketDto) {

  }
}
