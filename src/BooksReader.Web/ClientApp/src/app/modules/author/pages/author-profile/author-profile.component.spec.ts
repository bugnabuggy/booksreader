import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorProfileComponent } from './author-profile.component';
import { SharedModule } from '@br/shared/shared.module';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AuthorProfileComponent', () => {
  let component: AuthorProfileComponent;
  let fixture: ComponentFixture<AuthorProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorProfileComponent ],
      imports:[
        SharedModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
