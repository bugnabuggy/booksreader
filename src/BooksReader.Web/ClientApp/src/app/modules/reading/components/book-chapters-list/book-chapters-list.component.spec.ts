import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChaptersListComponent } from './book-chapters-list.component';
import { BookChapterInfoComponent } from '../book-chapter-info/book-chapter-info.component';
import { COMMON_VISUAL_MODULES, COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookChaptersListComponent', () => {
  let component: BookChaptersListComponent;
  let fixture: ComponentFixture<BookChaptersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookChaptersListComponent,
        BookChapterInfoComponent
       ],
       imports:[
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChaptersListComponent);
    component = fixture.componentInstance;
    component.chapters = [];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
