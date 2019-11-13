import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksListComponent } from './books-list.component';
import { BooksListItemComponent } from '../books-list-item/books-list-item.component';
import { COMMON_VISUAL_MODULES, COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BooksListComponent', () => {
  let component: BooksListComponent;
  let fixture: ComponentFixture<BooksListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BooksListComponent,
        BooksListItemComponent
       ],
       imports:[
         COMMON_VISUAL_MODULES,
         COMMON_TESTING_MODULES
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BooksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
