import { TestBed } from '@angular/core/testing';

import { PublicService } from './public.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PublicService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: PublicService = TestBed.get(PublicService);
    expect(service).toBeTruthy();
  });
});
