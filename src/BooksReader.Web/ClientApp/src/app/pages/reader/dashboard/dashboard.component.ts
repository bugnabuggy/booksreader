import { Component, OnInit, ViewChild } from '@angular/core';
import { SecurityService } from '../../../services/';
import { UserHubService } from '../../../hubs';
import { LogoutData, LoginHistory, WebResult } from '../../../models/api-contracts';
import { StandardFilters } from '../../../models/filters';
import { MatPaginator, MatSort } from '@angular/material';
import { SiteConstants } from '../../../enums';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  loginHistory: LoginHistory[];
  displayedColumns: string[] = ['dateTime', 'ipAddress', 'browser', 'geolocation'];
  currentFilters: StandardFilters = { pageSize: 10, pageNumber: 0, isDesc: true, orderByField: 'dateTime' };
  itemsPerPage = SiteConstants.itemsPerPage;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

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
