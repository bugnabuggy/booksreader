import { TestBed } from '@angular/core/testing';

import { BooksPricesService } from './books-prices.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BooksPricesService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: BooksPricesService = TestBed.get(BooksPricesService);
    expect(service).toBeTruthy();
  });
});
