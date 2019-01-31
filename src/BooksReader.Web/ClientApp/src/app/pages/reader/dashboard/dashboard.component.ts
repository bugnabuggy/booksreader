import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../../services/';
import { UserHubService } from '../../../hubs';
import { LogoutData, LogHistory } from '../../../models/api-contracts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  logHistory: LogHistory[];
  displayedColumns: string[] = ['dateTime', 'ipAddress', 'browser', 'geolocation'];
  constructor(
    public security: SecurityService,
    public userHub: UserHubService
  ) {
  }

  ngOnInit(): void {
    this.userHub.init();
    this.security.getLogHistory().subscribe( (val: LogHistory[]) => {
      this.logHistory = val;
    } );
  }


  sendMsg() {
    this.userHub.checkStats(this.security.user.name);
  }
}
