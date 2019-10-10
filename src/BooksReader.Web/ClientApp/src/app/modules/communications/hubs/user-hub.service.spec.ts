import { TestBed } from '@angular/core/testing';

import { UserHubService } from './user-hub.service';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('UserHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      COMMON_TESTING_MODULES
    ]
  }));

  it('should be created', () => {
    const service: UserHubService = TestBed.get(UserHubService);
    expect(service).toBeTruthy();
  });
});
