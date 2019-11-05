import { TestBed } from '@angular/core/testing';

import { AdminBookService } from './admin-book.service';

describe('AdminBookService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AdminBookService = TestBed.get(AdminBookService);
    expect(service).toBeTruthy();
  });
});
