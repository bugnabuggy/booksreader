import { TestBed } from '@angular/core/testing';

import { UserDomainsService } from './user-domains.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UserDomainsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[ HttpClientTestingModule]
  }));

  it('should be created', () => {
    const service: UserDomainsService = TestBed.get(UserDomainsService);
    expect(service).toBeTruthy();
  });
});
