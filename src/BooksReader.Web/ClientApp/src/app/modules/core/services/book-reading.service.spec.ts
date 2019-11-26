import { TestBed } from '@angular/core/testing';

import { BookReadingService } from './book-reading.service';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookReadingService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [COMMON_TESTING_MODULES]
  }));

  it('should be created', () => {
    const service: BookReadingService = TestBed.get(BookReadingService);
    expect(service).toBeTruthy();
  });
});
