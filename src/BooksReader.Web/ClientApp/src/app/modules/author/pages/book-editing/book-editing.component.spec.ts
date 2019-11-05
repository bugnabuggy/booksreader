import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SharedModule } from '@br/shared/shared.module';


import { 
        BookPropertiesComponent,
        BookChaptersEditorComponent, 
        BookContentInnerEditorComponent, 
        BookChapterInfoEditorComponent 
      } from '../../components';


import { EditorComponent } from '@tinymce/tinymce-angular';
import { 
        COMMON_TESTING_MODULES,
        COMMON_VISUAL_MODULES 
      } from '@br/test/common-dependencies-modules';

import { BookEditingComponent } from './book-editing.component';

import { 
        BookInfoEditComponent,
        BookContentEditorComponent 
      } from '..';

import { ReadingModule } from '@br/reading/reading.module';

describe('BookEditingComponent', () => {
  let component: BookEditingComponent;
  let fixture: ComponentFixture<BookEditingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookEditingComponent,
        BookInfoEditComponent,
        BookPropertiesComponent,
        BookContentEditorComponent,
        BookChaptersEditorComponent,
        BookChapterInfoEditorComponent,
        BookContentInnerEditorComponent,
        EditorComponent
      ],
      imports:[
        SharedModule,
        ReadingModule,
        COMMON_TESTING_MODULES,
        COMMON_VISUAL_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookEditingComponent);
    component = fixture.componentInstance;
    component.chapters = [];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
