import { TestBed } from '@angular/core/testing';

import { BookEditService } from './book-edit.service';

describe('BookEditService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookEditService = TestBed.get(BookEditService);
    expect(service).toBeTruthy();
  });
});
