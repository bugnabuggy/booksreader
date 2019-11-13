import { TestBed } from '@angular/core/testing';

import { BookMarketService } from './book-market.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BookMarketService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: BookMarketService = TestBed.get(BookMarketService);
    expect(service).toBeTruthy();
  });
});
