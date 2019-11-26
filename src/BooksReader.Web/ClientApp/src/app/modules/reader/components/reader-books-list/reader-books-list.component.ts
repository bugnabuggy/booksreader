import { Component, OnInit, Input } from '@angular/core';
import { ReaderDashboardDto, Action, ConfirmationDialogModel } from '@br/core/models';
import { ActionType, ConfirmationType, ConfirmationResult } from '@br/core/enums';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '@br/core/services';
import { ConfirmationDialogComponent } from '@br/controls/dialogs';
import { SiteMessages } from '@br/config/site-messages';
import { ReaderDashboardService } from '@br/reader/services';
import { TranslateService } from '@ngx-translate/core';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-reader-books-list',
  templateUrl: './reader-books-list.component.html',
  styleUrls: ['./reader-books-list.component.scss']
})
export class ReaderBooksListComponent implements OnInit {
  @Input() books: ReaderDashboardDto[] = [];

  actions = {};

  constructor(
    private dialog: MatDialog,
    private readerDashboardSvc: ReaderDashboardService,
    private notifications: NotificationService,
    private router: Router,
    private translate: TranslateService
  ) {
    this.actions[ActionType.select] = this.read.bind(this);
    this.actions[ActionType.delete] = this.remove.bind(this);
   }

  ngOnInit() {
  }

  doAction(action: Action<ReaderDashboardDto>) {
    let func = this.actions[action.type];
    
    if(func) {
      func(action.data);
    }
  }

  read( subscription: ReaderDashboardDto) {
    this.router.navigateByUrl(Endpoints.frontend.reader.readTheBookUrl.replace(':bookId', subscription.book.bookId));
  }

  remove(subscription: ReaderDashboardDto) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        type: ConfirmationType.yesNo,
        text: SiteMessages.reader.books.deleteQuestion
      } as ConfirmationDialogModel
    });

    dialogRef.afterClosed().subscribe(confirmresult  => {
      if (confirmresult == ConfirmationResult.yes) {
        this.readerDashboardSvc.removeSubscription(subscription)
        .subscribe(val=>{
          let index = this.books.findIndex(x=>x.subscription.subscriptionId == subscription.subscription.subscriptionId);
          this.books.splice(index, 1);
          this.notifications.showInfo(this.translate.instant(SiteMessages.reader.books.removed));
        });
      }
    });
  }
}
