import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChaptersEditorComponent } from './book-chapters-editor.component';
import { COMMON_TESTING_MODULES, 
  COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { BookChapter } from '@br/core/models';
import { BookChapterInfoEditorComponent } from '../book-chapter-info-editor/book-chapter-info-editor.component';

describe('BookChaptersEditorComponent', () => {
  let component: BookChaptersEditorComponent;
  let fixture: ComponentFixture<BookChaptersEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookChaptersEditorComponent,
        BookChapterInfoEditorComponent
       ],
      imports:[
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChaptersEditorComponent);
    component = fixture.componentInstance;
    component.chapters = [] as BookChapter[];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
