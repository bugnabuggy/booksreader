import { Component, OnInit } from '@angular/core';
import { BookEditingService, NotificationService } from '@br/core/services';
import { SiteMessages } from '@br/config/site-messages';
import { BooksFilters, Book, OperationResult, Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.scss']
})
export class AuthorDashboardComponent implements OnInit {

  filters: BooksFilters = { title: '', description: '' } as BooksFilters;
  books: Book[] = [];

  constructor(
    private bookSvc: BookEditingService,
    private notifications: NotificationService
  ) { 
  }

  ngOnInit() {
    this.bookSvc.getBooks(this.filters)
      .subscribe( val => {
        this.books = val.data;
      }, err => {
        this.notifications.showError(err, SiteMessages.author.books.errorLoadingList);
      })
  }

}
