import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookChapterInfoComponent } from './book-chapter-info.component';

describe('BookChapterInfoComponent', () => {
  let component: BookChapterInfoComponent;
  let fixture: ComponentFixture<BookChapterInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookChapterInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookChapterInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
