import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { provideConfig } from './utilities/social-config.provider';
import { SocialLoginService } from './services';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SocialLoginModule
  ],
  providers: [
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    },
    SocialLoginService
  ],
  exports: [
  ]
})
export class BrIntegrationsModule { }
