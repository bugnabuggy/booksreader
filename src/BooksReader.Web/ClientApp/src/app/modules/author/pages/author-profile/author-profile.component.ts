import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthorProfileService } from '@br/core/services';
import { UserDomain } from '@br/core/models';

@Component({
  selector: 'app-author-profile',
  templateUrl: './author-profile.component.html',
  styleUrls: ['./author-profile.component.scss']
})
export class AuthorProfileComponent  implements OnInit {
  uiIsBlocked = false;
  domainEditId = null;
  addInProgress = false;

  profileForm: FormGroup = this.fb.group({
    authorName: ['', Validators.required],
    description: ['']
  });

  domains: UserDomain[] = [ {
    id: '2341',
    name: 'ya.ru',
    protocol: 'http://',
    verified: false,
    verificationType: 2,
    verificationCode: 'qwerty'
  },
  {
    id: '5321',
    name: 'br.ruteco.com',
    protocol: 'https://',
    verified: true,
    verificationType: 1,
    verificationCode: 'qwerty'
  }];

  errors = [];

  constructor(
    private fb: FormBuilder, 
    public authorProfileSvc: AuthorProfileService
  ) { 
  }

  ngOnInit() {
    
  }

  save() {

  }
}
