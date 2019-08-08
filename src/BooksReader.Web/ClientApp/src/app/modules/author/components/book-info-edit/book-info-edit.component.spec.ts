import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookInfoEditComponent } from './book-info-edit.component';

describe('BookInfoEditComponent', () => {
  let component: BookInfoEditComponent;
  let fixture: ComponentFixture<BookInfoEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookInfoEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookInfoEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
