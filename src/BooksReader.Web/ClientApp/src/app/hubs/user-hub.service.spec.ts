import { TestBed } from '@angular/core/testing';

import { UserHubService } from './user-hub.service';
import { MOCKED_PROVIDERS } from '../../tests/mocks/mockedProviders';

describe('UserHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: MOCKED_PROVIDERS
  }));

  it('should be created', () => {
    const service: UserHubService = TestBed.get(UserHubService);
    expect(service).toBeTruthy();
  });
});
