import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookEditingPageComponent } from './book-editing-page.component';

describe('BookEditingPageComponent', () => {
  let component: BookEditingPageComponent;
  let fixture: ComponentFixture<BookEditingPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookEditingPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookEditingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
