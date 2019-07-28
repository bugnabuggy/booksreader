import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorProfilePageComponent } from './author-profile-page.component';
import { SharedModule } from '@br/shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { TranslateModule } from '@ngx-translate/core';
import { MockStorageService } from '@br/tests/mocks';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('AuthorProfilePageComponent', () => {
  let component: AuthorProfilePageComponent;
  let fixture: ComponentFixture<AuthorProfilePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorProfilePageComponent ],
      imports: [
        SharedModule,
        NoopAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        SimpleNotificationsModule.forRoot(),
        TranslateModule.forRoot()
      ],
      providers: [
        {
          provide: Storage,
          useValue: new MockStorageService()
        },
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
