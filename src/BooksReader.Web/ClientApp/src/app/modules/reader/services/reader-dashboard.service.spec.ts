import { TestBed } from '@angular/core/testing';

import { ReaderDashboardService } from './reader-dashboard.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ReaderDashboardService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: ReaderDashboardService = TestBed.get(ReaderDashboardService);
    expect(service).toBeTruthy();
  });
});
