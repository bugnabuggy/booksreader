import { TestBed } from '@angular/core/testing';

import { BookEditingService } from './book-editing.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BookEditingService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: BookEditingService = TestBed.get(BookEditingService);
    expect(service).toBeTruthy();
  });
});
