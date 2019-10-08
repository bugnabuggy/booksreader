import { Component, OnInit } from '@angular/core';
import { ListsService, UserService } from '@br/core/services/';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    private listsSvc: ListsService,
    public userSvc: UserService
  ) {
  }

  ngOnInit(): void {
    this.listsSvc.init();
    this.userSvc.init()
    .subscribe(val => {

    });
  }
}
