import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderBooksListItemComponent } from './reader-books-list-item.component';

describe('ReaderBooksListItemComponent', () => {
  let component: ReaderBooksListItemComponent;
  let fixture: ComponentFixture<ReaderBooksListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReaderBooksListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderBooksListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
