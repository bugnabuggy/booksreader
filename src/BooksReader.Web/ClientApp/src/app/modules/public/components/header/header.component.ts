import { Component, OnInit } from '@angular/core';
import { SecurityService, UserService } from '@br/core/services';
import { FormControl } from '@angular/forms';
import { Languages } from '@br/config/languages';
import { Language } from '@br/core/models';
import { Subscription } from 'rxjs';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  Endpoints = Endpoints;

  languages = Languages.get();
  language = new FormControl(this.languages[1]);
  langSub: Subscription;

  constructor(
    public security: SecurityService,
    public userSvc: UserService
  ) { }

  ngOnInit() {
    this.langSub = this.language.valueChanges.subscribe((val: Language) => {
      this.userSvc.changeLanguage(val);
      
    });

  }



}
