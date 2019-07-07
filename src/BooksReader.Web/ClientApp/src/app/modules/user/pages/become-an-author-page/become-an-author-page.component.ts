import { Component, OnInit } from '@angular/core';
import { UserService, NotificationService } from '@br/core/services';
import { AuthorData } from '@br/core/models/api-contracts/requests/author-data.request';

@Component({
  selector: 'app-become-an-author-page',
  templateUrl: './become-an-author-page.component.html',
  styleUrls: ['./become-an-author-page.component.scss']
})
export class BecomeAnAuthorPageComponent implements OnInit {
  authorData: AuthorData;


  constructor(
    public userSvc: UserService,
    public notifications: NotificationService
  ) { }

  ngOnInit() {
  }

  becomeAnAuthor(){
    this.authorData = {};

    this.userSvc.authorRequest(this.authorData)
    .subscribe(val => {
      debugger;
        this.userSvc.refresh();
      // redirect to author dashboard
    }, err => {
      debugger;
      this.notifications.showError(err.error || err.message);
    });
  }

}

