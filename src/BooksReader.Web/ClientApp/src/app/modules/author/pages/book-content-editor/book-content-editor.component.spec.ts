import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookContentEditorComponent } from './book-content-editor.component';
import { COMMON_VISUAL_MODULES, COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { BookChapter } from '@br/core/models';
import { BookChaptersEditorComponent, BookContentInnerEditorComponent, BookChapterInfoEditorComponent } from '../../components';
import { EditorComponent } from '@tinymce/tinymce-angular';

describe('BookContentEditorComponent', () => {
  let component: BookContentEditorComponent;
  let fixture: ComponentFixture<BookContentEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookContentEditorComponent,
        BookChaptersEditorComponent,
        BookChapterInfoEditorComponent,
        BookContentInnerEditorComponent,
        EditorComponent
      ],
      imports:[
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookContentEditorComponent);
    component = fixture.componentInstance;
    component.chapters = [] as BookChapter[];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
