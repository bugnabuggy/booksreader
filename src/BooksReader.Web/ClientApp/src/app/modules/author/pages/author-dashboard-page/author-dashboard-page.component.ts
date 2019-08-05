import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddBookDialogComponent } from '../../components';
import { BookEditingService, NotificationService } from '@br/core/services';
import { Book, OperationResult, AuthorBookFilters } from '@br/core/models';
import { StringConstants, Endpoints } from '@br/config';
import { BookRenderingType, BookSelectionType } from '@br/core/enums';
import { BookSelection } from '@br/core/models/site/events';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-dashboard-page',
  templateUrl: './author-dashboard-page.component.html',
  styleUrls: ['./author-dashboard-page.component.scss']
})
export class AuthorDashboardPageComponent implements OnInit {

  books: Book[] = [];
  bookRenderingType: BookRenderingType = BookRenderingType.author;

  filters: AuthorBookFilters = {
  };

  actions = {};

  constructor(
    public dialog: MatDialog,
    private bookEditingSvc: BookEditingService,
    private notifications: NotificationService,
    private router: Router
  ) {
    this.actions[BookSelectionType.select] = this.rename;
    this.actions[BookSelectionType.edit] = this.edit;
    this.actions[BookSelectionType.delete] = this.delete;
  }

  ngOnInit() {
    this.bookEditingSvc.list(this.filters)
      .subscribe(val => {
        this.books = val.data;
      },
        err => {
          this.notifications.showError(err.message || StringConstants.errors.anyError);
        });
  }


  selected(selection: BookSelection) {
    console.log(selection);
    let action = this.actions[selection.event];
    if (action) {
      action = action.bind(this);
      action(selection.book);
    }
  }


  add() {
    const dialogRef = this.dialog.open(AddBookDialogComponent, {
      minHeight: "50%",
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result) {
        this.bookEditingSvc.add({
          title: result
        } as Book)
          .subscribe((val: OperationResult<Book>) => {
            this.books.push(val.data);
            this.notifications.showSuccess(StringConstants.books.added)
          });
      }

    });
  }

  rename(book: Book) {
    const dialogRef = this.dialog.open(AddBookDialogComponent, {
      minHeight: "50%",
      data: book
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if (result) {
        book.title = result;
        this.bookEditingSvc.update(book)
          .subscribe((val: OperationResult<Book>) => {
            debugger;
            this.notifications.showSuccess(StringConstants.books.edited)
          });
      }
    });
  }

  edit(book: Book) {
    debugger;
    this.router.navigateByUrl(Endpoints.forntend.author.bookUrl.replace(':id', book.id));
  }

  delete(book: Book) {
    this.bookEditingSvc.delete(book.id)
    .subscribe(val=>{
      this.books = this.books.filter(x=>x.id != book.id);
      this.notifications.showSuccess(StringConstants.books.deleted)
      debugger;
    })
  }

}
