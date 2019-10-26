import { Component, OnInit } from '@angular/core';
import { UserDomain } from '@br/core/models';
import { AuthorProfileService } from '@br/core/services';

@Component({
  selector: 'app-author-container',
  templateUrl: './author-container.component.html',
  styleUrls: ['./author-container.component.scss']
})
export class AuthorContainerComponent implements OnInit {

  constructor(
    private authorProfileSvc: AuthorProfileService,
  ) { }

  ngOnInit() {
    this.authorProfileSvc.getFullProfile();
  }

}
