import { Component, OnInit } from '@angular/core';
import { AuthorProfileService, NotificationService } from '@br/core/services';
import { AuthorProfile } from '@br/core/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StringConstants } from '@br/config';

@Component({
  selector: 'app-author-profile-page',
  templateUrl: './author-profile-page.component.html',
  styleUrls: ['./author-profile-page.component.scss']
})
export class AuthorProfilePageComponent implements OnInit {
  profile: AuthorProfile;
  profileForm: FormGroup = this.fb.group({
      authorName: ['', Validators.required],
      description: [''],
      domainName: [''],
      urlPath: [''],
      pageContent: ['']
  });
  isUiBlocked = false;
  errors = [];

  constructor(
    public authorSvc: AuthorProfileService,
    public notifications: NotificationService,
    public fb: FormBuilder
  ) { }

  ngOnInit() {
    this.authorSvc.getAuthorProfile().subscribe(val => {
      this.profile = val;

      this.profileForm = this.fb.group({
        id: [val.id],
        authorName: [val.authorName, Validators.required],
        description: [val.description],
        domainName: [val.domainName],
        urlPath: [val.urlPath],
        pageContent: [val.pageContent]
      });

    }, err=>{
      debugger;
      this.notifications.showError(err.error);
    });
  }

  save() {
    this.errors = [];
    this.profile = this.profileForm.value as AuthorProfile;
    this.authorSvc.updateAuthorProfile(this.profile)
      .subscribe(val=>{
        this.notifications.showSuccess(StringConstants.profileUpdated);
      }, err=>{
        if (err.status == 0) {
          this.notifications.showError(err.name);
        } else {
          this.notifications.showError(err.error.messages[0]);
        }
        
      });
  }

}
