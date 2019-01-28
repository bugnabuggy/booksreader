import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../../services/';
import { UserHubService } from '../../../hubs';
import { LogoutData } from '../../../models/api-contracts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  constructor(
    public security: SecurityService,
    public userHub: UserHubService
  ) {
  }

  ngOnInit(): void {
    this.userHub.init();
  }


  sendMsg() {
    this.userHub.checkStats(this.security.user.name);
  }
}
