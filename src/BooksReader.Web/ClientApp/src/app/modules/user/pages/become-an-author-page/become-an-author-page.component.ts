import { Component, OnInit } from '@angular/core';
import { UserService, NotificationService } from '@br/core/services';
import { AuthorProfile } from '@br/core/models';
import { Router } from '@angular/router';
import { Endpoints } from '@br/config';



@Component({
  selector: 'app-become-an-author-page',
  templateUrl: './become-an-author-page.component.html',
  styleUrls: ['./become-an-author-page.component.scss']
})
export class BecomeAnAuthorPageComponent implements OnInit {
  authorData: AuthorProfile;


  constructor(
    public userSvc: UserService,
    public notifications: NotificationService,
    public router: Router
  ) { }

  ngOnInit() {
  }

  becomeAnAuthor(){
    this.authorData = { } as AuthorProfile;

    this.userSvc.authorRequest(this.authorData)
    .subscribe(val => {
        this.userSvc.refresh();
        this.router.navigateByUrl(Endpoints.forntend.author.profileUrl);
    }, err => {
      debugger;
      this.notifications.showError(err.error || err.message);
    });
  }

}

