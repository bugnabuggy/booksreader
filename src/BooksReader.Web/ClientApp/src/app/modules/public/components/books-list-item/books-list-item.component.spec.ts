import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksListItemComponent } from './books-list-item.component';
import { COMMON_TESTING_MODULES, COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { bookMarketDtoMock } from '@br/test/mock-data';

describe('BooksListItemComponent', () => {
  let component: BooksListItemComponent;
  let fixture: ComponentFixture<BooksListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BooksListItemComponent ],
      imports:[
        COMMON_VISUAL_MODULES,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BooksListItemComponent);
    component = fixture.componentInstance;
    component.book = bookMarketDtoMock;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
