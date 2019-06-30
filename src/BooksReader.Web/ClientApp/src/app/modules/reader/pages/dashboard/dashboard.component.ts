import { Component, OnInit, ViewChild } from '@angular/core';
import { SecurityService } from '@br/core/services';
import { UserHubService } from '@br/communications/hubs';
import { LogoutData, LoginHistory, WebResult } from '@br/core/models';
import { StandardFilters } from '@br/core/models';
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { SiteConstants } from '@br/config';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  loginHistory: LoginHistory[];
  displayedColumns: string[] = ['dateTime', 'ipAddress', 'browser', 'geolocation'];
  currentFilters: StandardFilters = { pageSize: 10, pageNumber: 0, isDesc: true, orderByField: 'dateTime' };
  itemsPerPage = SiteConstants.itemsPerPage;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(
    public security: SecurityService,
    public userHub: UserHubService
  ) {
  }

  ngOnInit(): void {
    this.userHub.init();
    this.getData(this.currentFilters);
  }
  getData(filters: StandardFilters) {
    this.security.getLogHistory(filters).subscribe((val: WebResult<LoginHistory[]>) => {
      this.loginHistory = val.data;
      this.paginator.length = val.total;
    });
  }


  sendMsg() {
    this.userHub.checkStats(this.security.user.name);
  }

  pageChanged(event) {
    this.currentFilters.pageNumber = event.pageIndex;
    this.currentFilters.pageSize = event.pageSize;
    this.getData(this.currentFilters);
  }

}
