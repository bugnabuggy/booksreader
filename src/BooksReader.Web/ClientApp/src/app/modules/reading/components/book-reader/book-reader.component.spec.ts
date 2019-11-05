import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookReaderComponent } from './book-reader.component';
import { COMMON_TESTING_MODULES, COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { BookChaptersListComponent } from '../book-chapters-list/book-chapters-list.component';
import { BookContentReaderComponent } from '../book-content-reader/book-content-reader.component';
import { BookChapterInfoComponent } from '../book-chapter-info/book-chapter-info.component';
import { CoreModule } from '@br/core/core.module';

describe('BookReaderComponent', () => {
  let component: BookReaderComponent;
  let fixture: ComponentFixture<BookReaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookReaderComponent,
        BookChaptersListComponent,
        BookContentReaderComponent,
        BookChapterInfoComponent
       ],
       imports:[
         CoreModule,
         COMMON_VISUAL_MODULES,
         COMMON_TESTING_MODULES
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookReaderComponent);
    component = fixture.componentInstance;
    component.chapters = [];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
