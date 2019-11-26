import { TestBed } from '@angular/core/testing';

import { BooksHubService } from './books-hub.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BooksHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: BooksHubService = TestBed.get(BooksHubService);
    expect(service).toBeTruthy();
  });
});
