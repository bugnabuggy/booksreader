import { TestBed } from '@angular/core/testing';

import { AdminBookService } from './admin-book.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminBookService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: AdminBookService = TestBed.get(AdminBookService);
    expect(service).toBeTruthy();
  });
});
