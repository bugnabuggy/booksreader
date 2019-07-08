import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDashboardPageComponent } from './author-dashboard-page.component';

import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@br/shared/shared.module';

describe('AuthorDashboardPageComponent', () => {
  let component: AuthorDashboardPageComponent;
  let fixture: ComponentFixture<AuthorDashboardPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        SharedModule,
        TranslateModule.forRoot()
      ],
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
