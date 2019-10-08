import { TestBed } from '@angular/core/testing';

import { ListsService } from './lists.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('ListsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: ListsService = TestBed.get(ListsService);
    expect(service).toBeTruthy();
  });
});
