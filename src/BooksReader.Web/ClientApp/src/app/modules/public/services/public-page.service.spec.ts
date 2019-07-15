import { TestBed } from '@angular/core/testing';

import { PublicPageService } from './public-page.service';

describe('PublicPageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PublicPageService = TestBed.get(PublicPageService);
    expect(service).toBeTruthy();
  });
});
