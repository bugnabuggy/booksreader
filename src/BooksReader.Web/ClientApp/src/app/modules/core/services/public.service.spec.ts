import { TestBed } from '@angular/core/testing';
import { Location } from '@angular/common';
import { PublicService } from './public.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';


let locationMock = { path: () => '/'}

describe('PublicService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
    ],
    providers:[
      { provide: Location, useValue: locationMock }
    ]
  }));

  it('should be created', () => {
    const service: PublicService = TestBed.get(PublicService);
    expect(service).toBeTruthy();
  });
});
