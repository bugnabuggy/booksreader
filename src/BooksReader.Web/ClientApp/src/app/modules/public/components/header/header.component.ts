import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Languages } from '@br/config';
import { Language } from '@br/core/models';
import { UserService } from '@br/core/services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  languages = Languages.get();
  language = new FormControl(this.languages.find(x=>x.code == this.userSvc.language));
  langSub: Subscription;

  menuSections = [] ;

  constructor(
    public userSvc: UserService
  ) { }

  ngOnInit() {
    this.langSub = this.language.valueChanges.subscribe((val: Language) => {
      this.userSvc.changeLanguage(val);
    });

    this.userSvc.menuSections$.subscribe(val=> {
      this.menuSections = val;
    });
  }
}
