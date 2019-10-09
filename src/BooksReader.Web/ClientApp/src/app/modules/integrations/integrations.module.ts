import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login';
import { provideConfig } from './utilities';
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
  exports:[
    SocialLoginModule,
  ]
})
export class IntegrationsModule { }
