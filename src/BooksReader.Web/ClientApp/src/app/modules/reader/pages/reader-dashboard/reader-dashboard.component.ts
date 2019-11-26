import { Component, OnInit } from '@angular/core';
import { ReaderDashboardService } from '@br/reader/services';
import { TabledPage } from '@br/shared/pages';
import { ReaderDashboardFilters, ReaderDashboardDto } from '@br/core/models';

@Component({
  selector: 'app-reader-dashboard',
  templateUrl: './reader-dashboard.component.html',
  styleUrls: ['./reader-dashboard.component.scss']
})
export class ReaderDashboardComponent extends TabledPage<ReaderDashboardDto, ReaderDashboardFilters> implements OnInit {

  constructor(
    private readerDashboardSvc: ReaderDashboardService,
  ) { 
    super();
  }

  ngOnInit() {
    this.getData(this.filters);
  }

  getData(filters) {
    this.readerDashboardSvc.getReaderBooks(filters).subscribe( val => {
      this.totalRecords = val.total;
      this.data = val.data;
    });
  }
}
