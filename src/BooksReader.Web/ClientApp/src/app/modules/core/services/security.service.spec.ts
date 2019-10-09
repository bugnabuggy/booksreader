import { TestBed } from '@angular/core/testing';

import { SecurityService } from './security.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StorageService } from './storage.service';


let storageSpy = {
  getItem: (key) => {
    return "" ;
  },
} as Storage;


describe('SecurityService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      RouterTestingModule,
      HttpClientTestingModule,
    ],
    providers: [
      { provide: StorageService, useValue: storageSpy }
    ]
  }));

  it('should be created', () => {
    const service: SecurityService = TestBed.get(SecurityService);
    expect(service).toBeTruthy();
  });
});
