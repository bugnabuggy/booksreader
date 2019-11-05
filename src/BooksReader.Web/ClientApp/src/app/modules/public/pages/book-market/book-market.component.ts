import { Component, OnInit } from '@angular/core';
import { BookMarketService } from '@br/core/book-market.service';
import { Subscription, pipe } from 'rxjs';
import { BooksFilters } from '@br/core/models';
import { SiteConstants } from '@br/config';
import { finalize } from 'rxjs/operators';
import { NotificationService } from '@br/core/services';

@Component({
  selector: 'app-book-market',
  templateUrl: './book-market.component.html',
  styleUrls: ['./book-market.component.scss']
})
export class BookMarketComponent implements OnInit {

  books = [];
  uiIsBlocked = false;
  totalRecords = 0;

  filters = {
    pageNumber: 0,
    pageSize: SiteConstants.defaultPageSize
  } as BooksFilters;

  constructor(
    private bookMarketSvc: BookMarketService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    debugger;
    this.getData(this.filters);
  }

  getData(filters) {
    this.bookMarketSvc.get(filters)
    .pipe(finalize(()=>{
      this.uiIsBlocked = false;
    }))
    .subscribe(val=>{
      this.totalRecords = val.total;
      this.books = val.data;
    }, err=> {
      this.notifications.showError(err);
    });
  }

  pageChanged(event){
    this.filters.pageSize = event.pageSize;
    this.filters.pageNumber = event.pageNumber;
    this.getData(this.filters);
  };
}
