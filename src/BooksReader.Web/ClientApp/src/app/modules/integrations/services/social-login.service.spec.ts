import { TestBed } from '@angular/core/testing';

import { SocialLoginService } from './social-login.service';
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login';
import { CoreModule } from '@br/core/core.module';
import { provideConfig } from '../utilities';

describe('SocialLoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      CoreModule,
      SocialLoginModule,
    ],
    providers:[
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
