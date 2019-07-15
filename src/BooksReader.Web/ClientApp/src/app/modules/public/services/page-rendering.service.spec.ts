import { TestBed } from '@angular/core/testing';

import { PageRenderingService } from './page-rendering.service';

describe('PageRenderingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PageRenderingService = TestBed.get(PageRenderingService);
    expect(service).toBeTruthy();
  });
});
