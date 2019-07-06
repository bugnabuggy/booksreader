import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BecomeAnAuthorPageComponent } from './become-an-author-page.component';

describe('BecomeAnAuthorPageComponent', () => {
  let component: BecomeAnAuthorPageComponent;
  let fixture: ComponentFixture<BecomeAnAuthorPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BecomeAnAuthorPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BecomeAnAuthorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
