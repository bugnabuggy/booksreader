import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksListNoContentComponent } from './books-list-no-content.component';

describe('BooksListNoContentComponent', () => {
  let component: BooksListNoContentComponent;
  let fixture: ComponentFixture<BooksListNoContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BooksListNoContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BooksListNoContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
