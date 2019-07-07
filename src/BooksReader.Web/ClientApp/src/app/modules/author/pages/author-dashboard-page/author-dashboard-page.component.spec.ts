import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDashboardPageComponent } from './author-dashboard-page.component';

describe('AuthorDashboardPageComponent', () => {
  let component: AuthorDashboardPageComponent;
  let fixture: ComponentFixture<AuthorDashboardPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorDashboardPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorDashboardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
