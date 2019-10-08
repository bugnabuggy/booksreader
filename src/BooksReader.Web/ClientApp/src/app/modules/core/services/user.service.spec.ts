import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import { NotificationService } from './notification.service';

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      { provide: NotificationService, useValue: {} }
    ]
  }));

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });
});
