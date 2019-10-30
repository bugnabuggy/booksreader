import { TestBed } from '@angular/core/testing';

import { BookChapterEditingService } from './book-chapter-editing.service';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookChapterEditingService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      COMMON_TESTING_MODULES
    ]
  }));

  it('should be created', () => {
    const service: BookChapterEditingService = TestBed.get(BookChapterEditingService);
    expect(service).toBeTruthy();
  });
});
