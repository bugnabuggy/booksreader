import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginOrRegisterComponent } from './login-or-register.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('LoginOrRegisterComponent', () => {
  let component: LoginOrRegisterComponent;
  let fixture: ComponentFixture<LoginOrRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginOrRegisterComponent ],
      imports: [
        COMMON_TESTING_MODULES
      ],
      providers:[
        { provide: MatDialogRef, useValue: {}},
        { provide: MAT_DIALOG_DATA, useValue: {}},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginOrRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
