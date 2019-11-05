import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Languages, Endpoints } from '@br/config';
import { Language } from '@br/core/models';
import { UserService } from '@br/core/services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  languages = Languages.get();
  language = new FormControl(this.languages.find(x=>x.code == this.userSvc.language));
  langSub: Subscription;
  menuSub: Subscription;

  menuSections = [] ;

  Urls = Endpoints.frontend;

  constructor(
    public userSvc: UserService
  ) { }

  ngOnInit() {
    this.langSub = this.language.valueChanges.subscribe((val: Language) => {
      this.userSvc.changeLanguage(val);
    });

    this.menuSub = this.userSvc.menuSections$.subscribe(val=> {
      this.menuSections = val;
    });
  }

  ngOnDestroy() {
    this.langSub.unsubscribe();
    this.menuSub.unsubscribe();
  }
}
