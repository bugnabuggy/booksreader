import { TestBed } from '@angular/core/testing';

import { AuthorProfileService } from './author-profile.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AuthorProfileService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[ HttpClientTestingModule]
  }));

  it('should be created', () => {
    const service: AuthorProfileService = TestBed.get(AuthorProfileService);
    expect(service).toBeTruthy();
  });
});
