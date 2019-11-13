import { Component, OnInit } from '@angular/core';
import { BookMarketService } from '@br/core/services/book-market.service';
import { BooksFilters } from '@br/core/models';
import { finalize } from 'rxjs/operators';
import { NotificationService } from '@br/core/services';
import { TabledPage } from '@br/shared/pages';
import { BookMarketDto } from '@br/core/models/api/dto/public';

@Component({
  selector: 'app-book-market',
  templateUrl: './book-market.component.html',
  styleUrls: ['./book-market.component.scss']
})
export class BookMarketComponent extends TabledPage<BookMarketDto, BooksFilters>  implements OnInit {

  constructor(
    private bookMarketSvc: BookMarketService,
    private notifications: NotificationService
  ) { 
    super()
  }
  
  ngOnInit() {
    this.getData(this.filters);
  }

  getData(filters) {
    this.bookMarketSvc.list(filters)
    .pipe(finalize(()=>{
      this.uiIsBlocked = false;
    }))
    .subscribe(val=>{
      this.totalRecords = val.total;
      this.data = val.data;
    }, err=> {
      this.notifications.showError(err);
    });
  }
  
}
