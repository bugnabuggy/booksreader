import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileComponent } from './profile.component';
import { SharedModule } from '@br/shared/shared.module';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MockStorageService } from '@br/tests/mocks';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { UserService } from '@br/core/services';
import { appUser } from '@br/tests/mocks/responses';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        NoopAnimationsModule,
        SharedModule,
        RouterTestingModule,
        HttpClientTestingModule,
        TranslateModule.forRoot(),
        SimpleNotificationsModule.forRoot()
      ],
      providers: [
        { provide: Storage, useValue: new MockStorageService() },
        { provide: UserService, useValue: {user: appUser } },
      ],
      declarations: [ ProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
