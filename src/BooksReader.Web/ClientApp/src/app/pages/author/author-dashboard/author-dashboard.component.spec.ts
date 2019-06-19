import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDashboardComponent } from './author-dashboard.component';
import { MOCKED_PROVIDERS } from '../../../../tests/mocks/mockedProviders';
import { MaterialModule } from 'src/app/modules/material/material.module';

describe('AuthorDashboardComponent', () => {
  let component: AuthorDashboardComponent;
  let fixture: ComponentFixture<AuthorDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorDashboardComponent ],
      imports: [ MaterialModule],
      providers: MOCKED_PROVIDERS
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
