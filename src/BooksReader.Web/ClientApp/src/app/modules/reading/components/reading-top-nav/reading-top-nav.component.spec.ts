import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadingTopNavComponent } from './reading-top-nav.component';

describe('ReadingTopNavComponent', () => {
  let component: ReadingTopNavComponent;
  let fixture: ComponentFixture<ReadingTopNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadingTopNavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadingTopNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
