import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ForceLogoutComponent } from './force-logout.component';
import { MOCKED_PROVIDERS } from '@br/tests/mocks';

describe('ForceLogoutComponent', () => {
  let component: ForceLogoutComponent;
  let fixture: ComponentFixture<ForceLogoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ForceLogoutComponent ],
      providers: MOCKED_PROVIDERS
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
