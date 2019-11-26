import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderBooksListComponent } from './reader-books-list.component';
import { COMMON_VISUAL_MODULES, COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { ReaderBooksListItemComponent } from '../reader-books-list-item/reader-books-list-item.component';

describe('ReaderBooksListComponent', () => {
  let component: ReaderBooksListComponent;
  let fixture: ComponentFixture<ReaderBooksListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        ReaderBooksListComponent,
        ReaderBooksListItemComponent
       ],
      imports: [
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES,
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderBooksListComponent);
    component = fixture.componentInstance;
    component.books = [];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
