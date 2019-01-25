import { Component, OnInit } from '@angular/core';
import { SecurityService, UserService } from './services';
import { UserHubService } from './hubs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(
    public security: SecurityService,
    public userSvc: UserService
  ) {}

  ngOnInit() {
    this.userSvc.init();
  }
}
