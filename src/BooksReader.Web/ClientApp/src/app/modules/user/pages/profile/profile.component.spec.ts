import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileComponent } from './profile.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { SharedModule } from '@br/shared/shared.module';
import { UserService } from '@br/core/services';
import { AppUser } from '@br/core/models';

let userSvc = {
  user: { 
    roles: []
  } as AppUser
}

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileComponent ],
      imports: [
        SharedModule,
        COMMON_TESTING_MODULES
      ],
      providers: [
        { provide: UserService, useValue: userSvc }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
