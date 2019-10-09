import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderDashboardComponent } from './reader-dashboard.component';

describe('ReaderDashboardComponent', () => {
  let component: ReaderDashboardComponent;
  let fixture: ComponentFixture<ReaderDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReaderDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
