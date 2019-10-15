import { TestBed } from '@angular/core/testing';

import { PublicPagesService } from './public-pages.service';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('PublicPagesService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      COMMON_TESTING_MODULES
    ]
  }));

  it('should be created', () => {
    const service: PublicPagesService = TestBed.get(PublicPagesService);
    expect(service).toBeTruthy();
  });
});
