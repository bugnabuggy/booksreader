import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderComponent } from './header.component';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { UserService } from '@br/core/services';
import { BehaviorSubject } from 'rxjs';

let userSvc = {
  menuSections$: new BehaviorSubject<any>([]),
  changeLanguage: (lang)=> { } 
} as UserService;

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeaderComponent ],
      imports: [
        SharedModule,
        TranslateModule.forRoot(),
        RouterTestingModule
      ],
      providers: [
        {provide: UserService, useValue: userSvc}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
