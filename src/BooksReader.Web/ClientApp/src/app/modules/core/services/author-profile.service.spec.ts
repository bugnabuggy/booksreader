import { TestBed } from '@angular/core/testing';

import { AuthorProfileService } from './author-profile.service';

describe('AuthorProfileService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthorProfileService = TestBed.get(AuthorProfileService);
    expect(service).toBeTruthy();
  });
});
