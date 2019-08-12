import { TestBed } from '@angular/core/testing';

import { BookChapterEditingService } from './book-chapter-editing.service';

describe('BookChapterEditingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookChapterEditingService = TestBed.get(BookChapterEditingService);
    expect(service).toBeTruthy();
  });
});
