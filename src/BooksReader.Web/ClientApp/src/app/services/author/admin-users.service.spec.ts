import { TestBed } from '@angular/core/testing';

import { AdminUsersService } from '../admin/admin-users.service';
import { MOCKED_PROVIDERS } from '../../../tests/mocks/mockedProviders';

describe('AdminUsersService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: MOCKED_PROVIDERS
  }));

  it('should be created', () => {
    const service: AdminUsersService = TestBed.get(AdminUsersService);
    expect(service).toBeTruthy();
  });
});
