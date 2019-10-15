import { TestBed } from '@angular/core/testing';

import { NotificationService } from './notification.service';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { TranslateModule } from '@ngx-translate/core';

describe('NotificationService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      SimpleNotificationsModule.forRoot(),
      TranslateModule.forRoot()
    ]
  }));

  it('should be created', () => {
    const service: NotificationService = TestBed.get(NotificationService);
    expect(service).toBeTruthy();
  });
});
