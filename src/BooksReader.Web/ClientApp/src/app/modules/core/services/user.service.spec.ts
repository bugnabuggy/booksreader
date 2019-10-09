import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import { NotificationService } from './notification.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      RouterTestingModule,
      HttpClientTestingModule,
      TranslateModule.forRoot()
    ],
    providers: [
      { provide: NotificationService, useValue: {} }
    ]
  }));

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });
});
