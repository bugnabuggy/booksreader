import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { MOCKED_PROVIDERS } from '../../../../../tests/mocks/mockedProviders';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/modules/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { CoreModule } from '@br/core/core.module';

describe('loginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [
          SharedModule,
          CoreModule,
          MaterialModule, 
          BrowserAnimationsModule
        ],
      providers: MOCKED_PROVIDERS
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
