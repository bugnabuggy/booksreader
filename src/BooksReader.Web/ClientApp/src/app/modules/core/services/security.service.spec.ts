import { TestBed } from '@angular/core/testing';

import { SecurityService } from './security.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StorageService } from './storage.service';

describe('SecurityService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      RouterTestingModule,
      HttpClientTestingModule,
    ],
    providers: [
      { provide: StorageService, useValue: { test: 1} }
    ]
  }));

  it('should be created', () => {
    debugger;
    const service: SecurityService = TestBed.get(SecurityService);
    expect(service).toBeTruthy();
  });
});
