import { Component, OnInit } from '@angular/core';
import { AuthorRequest } from '@br/core/models';
import { UserService, NotificationService } from '@br/core/services';
import { Router } from '@angular/router';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-become-an-author',
  templateUrl: './become-an-author.component.html',
  styleUrls: ['./become-an-author.component.scss']
})
export class BecomeAnAuthorComponent implements OnInit {
  authorData: AuthorRequest;

  constructor(
    public userSvc: UserService,
    public notifications: NotificationService,
    public router: Router
  ) { }

  ngOnInit() {
  }

  becomeAnAuthor() {
    this.authorData = { } as AuthorRequest;

    this.userSvc.authorRequest(this.authorData)
    .subscribe(val => {
        this.userSvc.refresh();
        this.router.navigateByUrl(Endpoints.frontend.author.profileUrl);
    }, err => {
      this.notifications.showError(err.error || err.message);
    });
  }
}
