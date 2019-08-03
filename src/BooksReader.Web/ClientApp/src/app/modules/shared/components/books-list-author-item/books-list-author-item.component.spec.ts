import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksListAuthorItemComponent } from './books-list-author-item.component';

describe('BooksListAuthorItemComponent', () => {
  let component: BooksListAuthorItemComponent;
  let fixture: ComponentFixture<BooksListAuthorItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BooksListAuthorItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BooksListAuthorItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
