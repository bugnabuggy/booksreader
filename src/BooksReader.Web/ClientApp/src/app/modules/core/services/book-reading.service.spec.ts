import { TestBed } from '@angular/core/testing';

import { BookReadingService } from './book-reading.service';

describe('BookReadingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookReadingService = TestBed.get(BookReadingService);
    expect(service).toBeTruthy();
  });
});
