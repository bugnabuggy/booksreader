import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookContentInnerEditorComponent } from './book-content-inner-editor.component';

describe('BookContentInnerEditorComponent', () => {
  let component: BookContentInnerEditorComponent;
  let fixture: ComponentFixture<BookContentInnerEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookContentInnerEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookContentInnerEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
