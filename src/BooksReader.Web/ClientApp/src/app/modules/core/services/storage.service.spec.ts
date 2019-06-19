import { TestBed } from '@angular/core/testing';

import { StorageService } from './storage.service';
import { MockStorageService } from '@br/tests/mocks/services';

describe('StorageService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: Storage,
        useValue: new MockStorageService()
      }
    ]
  }));

  it('should be created', () => {
    const service: StorageService = TestBed.get(StorageService);
    expect(service).toBeTruthy();
  });
});
