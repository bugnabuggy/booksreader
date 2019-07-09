import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/modules/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { CoreModule } from '@br/core/core.module';
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login';
import { provideConfig } from '@br/integrations/utilities';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MockStorageService } from '@br/tests/mocks';
import { TranslateModule } from '@ngx-translate/core';
import { SimpleNotificationsModule } from 'angular2-notifications';

describe('loginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [
          SharedModule,
          RouterTestingModule,
          HttpClientTestingModule,
          MaterialModule, 
          NoopAnimationsModule,
          SocialLoginModule,
          TranslateModule.forRoot(),
          SimpleNotificationsModule.forRoot()
      ],
      providers:[
        { provide: AuthServiceConfig, useFactory: provideConfig },
        { provide: Storage, useValue: new MockStorageService() }
      ]
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
