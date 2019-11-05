import { Component, OnInit } from '@angular/core';
import { TabledPage } from '@br/shared/pages';
import { AdminBookService } from '@br/core/services';
import { AdminBookDto } from '@br/core/models';
import { AdminAllBooksFilter } from '@br/core/models/api/requests/admin';
import { finalize } from 'rxjs/operators';
import { Overlay } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-all-books',
  templateUrl: './all-books.component.html',
  styleUrls: ['./all-books.component.scss']
})
export class AllBooksComponent extends TabledPage<AdminBookDto, AdminAllBooksFilter> implements OnInit {

  columns = [
    'bookTitle',
    'username',
    'created',
    'isPublished',
    'published',
    'verified',
    'actions'
  ];
  
  Urls = Endpoints.frontend.admin;

  constructor(
    private adminBooksSvc: AdminBookService,
  ) {
    super();

    this.filters = {
      pageSize: 50, 
      pageNumber: 0
    };
   }

  ngOnInit() {
    this.getData(this.filters)
    ;
  }

  getData(filters: AdminAllBooksFilter) {
    this.uiIsBlocked = true;

    this.adminBooksSvc.list(filters)
    .pipe(finalize(()=>{
      this.uiIsBlocked = false;
    }))
    .subscribe(val => {
      this.data = val.data;
      this.totalRecords = val.total
      
    });
  }

  toggle(item: AdminBookDto) {
    this.uiIsBlocked = true;
    
    this.adminBooksSvc.toggleVerification(item)
      .pipe(finalize(()=>{
          this.uiIsBlocked = false;
      }))
      .subscribe(val => {
        item.verified = !item.verified;

      
      });
      
  }
}
