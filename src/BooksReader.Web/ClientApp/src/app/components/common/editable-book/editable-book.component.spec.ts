import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditableBookComponent } from './editable-book.component';

describe('EditableBookComponent', () => {
  let component: EditableBookComponent;
  let fixture: ComponentFixture<EditableBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditableBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditableBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
