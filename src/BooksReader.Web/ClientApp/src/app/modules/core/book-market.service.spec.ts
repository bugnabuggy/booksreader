import { TestBed } from '@angular/core/testing';

import { BookMarketService } from './book-market.service';

describe('BookMarketService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookMarketService = TestBed.get(BookMarketService);
    expect(service).toBeTruthy();
  });
});
