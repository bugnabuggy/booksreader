import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderBooksListComponent } from './reader-books-list.component';

describe('ReaderBooksListComponent', () => {
  let component: ReaderBooksListComponent;
  let fixture: ComponentFixture<ReaderBooksListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReaderBooksListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderBooksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
