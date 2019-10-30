import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookContentInnerEditorComponent } from './book-content-inner-editor.component';
import { EditorComponent } from '@tinymce/tinymce-angular';
import { COMMON_VISUAL_MODULES, COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookContentInnerEditorComponent', () => {
  let component: BookContentInnerEditorComponent;
  let fixture: ComponentFixture<BookContentInnerEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        BookContentInnerEditorComponent,
        EditorComponent
      ],
      imports: [
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookContentInnerEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
