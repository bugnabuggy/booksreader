import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllUsersPageComponent } from './all-users-page.component';
import { MaterialModule } from '@br/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CoreModule } from '@br/core/core.module';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('AllUsersPageComponent', () => {
  let component: AllUsersPageComponent;
  let fixture: ComponentFixture<AllUsersPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        NoopAnimationsModule,
        MaterialModule,
        RouterTestingModule,
        HttpClientTestingModule,
        TranslateModule.forRoot(),
        SimpleNotificationsModule.forRoot()
      ],
      declarations: [ AllUsersPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
