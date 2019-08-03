import { TestBed } from '@angular/core/testing';

import { BookEditingService } from './book-editing.service';

describe('BookEditingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookEditingService = TestBed.get(BookEditingService);
    expect(service).toBeTruthy();
  });
});
