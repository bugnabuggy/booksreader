import { TestBed } from '@angular/core/testing';

import { AdminUsersService } from './admin-users.service';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AdminUsersService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      COMMON_TESTING_MODULES
    ]
  }));

  it('should be created', () => {
    const service: AdminUsersService = TestBed.get(AdminUsersService);
    expect(service).toBeTruthy();
  });
});
