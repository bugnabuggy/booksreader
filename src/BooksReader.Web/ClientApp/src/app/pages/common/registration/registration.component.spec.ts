import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RegistrationComponent } from './registration.component';
import { MOCKED_PROVIDERS } from '../../../../tests/mocks/mockedProviders';
import { MATERIAL_DESIGN } from '../../../moduleExports';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('registrationComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationComponent ],
      imports: [ FormsModule, MATERIAL_DESIGN, BrowserAnimationsModule ],
      providers: MOCKED_PROVIDERS
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
