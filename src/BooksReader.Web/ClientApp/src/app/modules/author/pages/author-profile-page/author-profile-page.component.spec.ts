import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorProfilePageComponent } from './author-profile-page.component';

describe('AuthorProfilePageComponent', () => {
  let component: AuthorProfilePageComponent;
  let fixture: ComponentFixture<AuthorProfilePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorProfilePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
