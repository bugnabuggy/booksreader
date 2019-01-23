import { Component } from '@angular/core';
import { SecurityService } from '../../services/';
import { UserHubService } from '../../hubs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  constructor(
    public security: SecurityService,
    public userHub: UserHubService
  ) {
  }

  sendMsg() {
    this.userHub.checkStats(this.security.user.name);
  }
}
