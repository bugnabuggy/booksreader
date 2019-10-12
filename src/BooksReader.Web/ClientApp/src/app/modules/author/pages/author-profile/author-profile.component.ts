import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthorProfileService, NotificationService } from '@br/core/services';
import { UserDomain, PublicPage } from '@br/core/models';
import { getErrorMessage } from '@br/utilities/error-extractor';
import { AuthorFullProfile } from '@br/core/models/api/dto/author/author-full-profile.dto';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-author-profile',
  templateUrl: './author-profile.component.html',
  styleUrls: ['./author-profile.component.scss']
})
export class AuthorProfileComponent  implements OnInit {
  uiIsBlocked = true;
  domainEditId = null;
  addInProgress = false;

  profileForm: FormGroup = this.fb.group({
    authorName: ['', Validators.required],
    description: ['']
  });

  page: PublicPage = null;
  domains: UserDomain[] = [];

  errors = [];

  constructor(
    private fb: FormBuilder, 
    private authorProfileSvc: AuthorProfileService,
    private notifications: NotificationService
  ) { 
  }

  ngOnInit() {
    this.authorProfileSvc.getFullProfile()
    .pipe(finalize( () => {
      this.uiIsBlocked = false;
    }))
    .subscribe((val: AuthorFullProfile) => {
      this.domains = val.domains;
      this.page = val.page;

      this.profileForm = this.fb.group({
        authorName: [val.authorName, Validators.required],
        description: [val.description]
      });
    }, 
    err => {
      const msg = getErrorMessage(err);
      this.notifications.showError(msg);
    })
  }

  save() {

  }
}
