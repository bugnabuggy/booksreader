import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorContainerComponent } from './author-container.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AuthorContainerComponent', () => {
  let component: AuthorContainerComponent;
  let fixture: ComponentFixture<AuthorContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorContainerComponent ],
      imports: [
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
