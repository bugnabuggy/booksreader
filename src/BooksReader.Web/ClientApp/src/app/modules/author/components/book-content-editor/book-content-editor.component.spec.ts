import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookContentEditorComponent } from './book-content-editor.component';

describe('BookContentEditorComponent', () => {
  let component: BookContentEditorComponent;
  let fixture: ComponentFixture<BookContentEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookContentEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookContentEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
