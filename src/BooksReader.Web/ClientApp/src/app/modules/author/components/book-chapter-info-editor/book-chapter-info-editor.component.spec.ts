import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChapterInfoEditorComponent } from './book-chapter-info-editor.component';

describe('BookChapterInfoEditorComponent', () => {
  let component: BookChapterInfoEditorComponent;
  let fixture: ComponentFixture<BookChapterInfoEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookChapterInfoEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChapterInfoEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
