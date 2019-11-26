import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookReaderComponent } from './book-reader.component';
import { CoreModule } from '@br/core/core.module';
import { COMMON_TESTING_MODULES, COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { BookChaptersListComponent } from '../book-chapters-list/book-chapters-list.component';
import { BookContentReaderComponent } from '../book-content-reader/book-content-reader.component';
import { BookChapterInfoComponent } from '../book-chapter-info/book-chapter-info.component';
import { ReadingTopNavComponent } from '../reading-top-nav/reading-top-nav.component';

describe('BookReaderComponent', () => {
  let component: BookReaderComponent;
  let fixture: ComponentFixture<BookReaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookReaderComponent,
        BookChaptersListComponent,
        BookContentReaderComponent,
        BookChapterInfoComponent,
        ReadingTopNavComponent
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
