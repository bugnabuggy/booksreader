import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderDashboardComponent } from './reader-dashboard.component';
import { MOCKED_PROVIDERS } from '@br/tests/mocks/mockedProviders';

describe('dashboardComponent', () => {
  let component: ReaderDashboardComponent;
  let fixture: ComponentFixture<ReaderDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReaderDashboardComponent ],
      providers: MOCKED_PROVIDERS
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
