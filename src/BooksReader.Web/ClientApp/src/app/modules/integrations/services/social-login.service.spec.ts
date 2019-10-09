import { TestBed } from '@angular/core/testing';

import { SocialLoginService } from './social-login.service';
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login';
import { provideConfig } from '../utilities';

describe('SocialLoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      SocialLoginModule,
    ],
    providers: [
      {
        provide: AuthServiceConfig,
        useFactory: provideConfig
      },
    ]
  }));

  it('should be created', () => {
    const service: SocialLoginService = TestBed.get(SocialLoginService);
    expect(service).toBeTruthy();
  });
});
