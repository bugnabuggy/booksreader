import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookReaderPreviewComponent } from './book-reader-preview.component';

describe('BookReaderPreviewComponent', () => {
  let component: BookReaderPreviewComponent;
  let fixture: ComponentFixture<BookReaderPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookReaderPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookReaderPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
