import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BecomeAnAuthorPageComponent } from './become-an-author-page.component';
import { MaterialModule } from '@br/material/material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CoreModule } from '@br/core/core.module';
import { MockStorageService } from '@br/tests/mocks';
import { StorageService } from '@br/core/services';
import { SharedModule } from '@br/shared/shared.module';
import { SimpleNotificationsModule } from 'angular2-notifications';

describe('BecomeAnAuthorPageComponent', () => {
  let component: BecomeAnAuthorPageComponent;
  let fixture: ComponentFixture<BecomeAnAuthorPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        CoreModule,
        MaterialModule,
        SharedModule,
        RouterTestingModule,
        HttpClientTestingModule,
        SimpleNotificationsModule.forRoot(),
        TranslateModule.forRoot()
      ],
      declarations: [ BecomeAnAuthorPageComponent ],
      providers:[
        { provide: StorageService, useValue: new MockStorageService() }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BecomeAnAuthorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
