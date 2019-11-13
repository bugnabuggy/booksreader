import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookMarketComponent } from './book-market.component';
import { COMMON_TESTING_MODULES, COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { BooksListComponent, BooksListItemComponent } from '@br/public/components';

describe('BookMarketComponent', () => {
  let component: BookMarketComponent;
  let fixture: ComponentFixture<BookMarketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookMarketComponent,
        BooksListComponent,
        BooksListItemComponent
      ],
      imports:[
        COMMON_TESTING_MODULES,
        COMMON_VISUAL_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookMarketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
