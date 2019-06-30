import { TestBed } from '@angular/core/testing';

import { SocialLoginService } from './social-login.service';
import { SocialLoginModule } from 'angularx-social-login';

describe('SocialLoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      SocialLoginModule
    ]
  }));

  it('should be created', () => {
    const service: SocialLoginService = TestBed.get(SocialLoginService);
    expect(service).toBeTruthy();
  });
});
