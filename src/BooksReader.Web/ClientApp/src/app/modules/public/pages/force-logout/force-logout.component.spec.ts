import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ForceLogoutComponent } from './force-logout.component';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('ForceLogoutComponent', () => {
  let component: ForceLogoutComponent;
  let fixture: ComponentFixture<ForceLogoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ForceLogoutComponent ],
      imports:[ 
        SimpleNotificationsModule.forRoot(),
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ForceLogoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
