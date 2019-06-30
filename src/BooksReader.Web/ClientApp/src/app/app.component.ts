import { Component, OnInit } from '@angular/core';
import { SecurityService, UserService } from '@br/core/services';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(
    public userSvc: UserService
  ) {}

  ngOnInit() {
    this.userSvc.init();
  }
}
