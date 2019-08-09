import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChaptersEditorComponent } from './book-chapters-editor.component';

describe('BookChaptersEditorComponent', () => {
  let component: BookChaptersEditorComponent;
  let fixture: ComponentFixture<BookChaptersEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookChaptersEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChaptersEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
