import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChapterInfoEditorComponent } from './book-chapter-info-editor.component';
import { COMMON_TESTING_MODULES, COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { BookChapter } from '@br/core/models';
import { bookChapterMock } from '@br/test/mock-data/book-chapter.mock';

describe('BookChapterInfoEditorComponent', () => {
  let component: BookChapterInfoEditorComponent;
  let fixture: ComponentFixture<BookChapterInfoEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookChapterInfoEditorComponent ],
      imports:[
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChapterInfoEditorComponent);
    component = fixture.componentInstance;
    component.chapter = bookChapterMock;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
