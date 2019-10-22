import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDashboardComponent } from './author-dashboard.component';
import { MaterialModule } from '@br/material/material.module';
import { AuthorBooksListComponent, AuthorBooksListItemComponent } from '../../components';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AuthorDashboardComponent', () => {
  let component: AuthorDashboardComponent;
  let fixture: ComponentFixture<AuthorDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        AuthorDashboardComponent,
        AuthorBooksListComponent,
        AuthorBooksListItemComponent
      ],
      imports:[
        MaterialModule,
        HttpClientTestingModule,
        COMMON_TESTING_MODULES
      ]
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
