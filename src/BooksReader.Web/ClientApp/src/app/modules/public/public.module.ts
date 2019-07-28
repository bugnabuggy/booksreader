import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { LoginComponent, RegistrationComponent, ForceLogoutComponent, LandingPageComponent } from './pages';
import { SharedModule } from '@br/shared/shared.module';
import { HeaderComponent } from './components/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { MainComponent } from './pages/main/main.component';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { BuyButtonComponent } from './components/buy-button/buy-button.component';

@NgModule({
  declarations: [
    HeaderComponent,

    LoginComponent,
    RegistrationComponent,
    MainComponent,
    ForceLogoutComponent,
    LandingPageComponent,
    LoginButtonComponent,
    BuyButtonComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    TranslateModule.forChild(),
    PublicRoutingModule
  ],
  exports:[
    HeaderComponent,
    LoginButtonComponent,
    BuyButtonComponent,

    LoginComponent,
    RegistrationComponent,
    ForceLogoutComponent,
    
  ]
})
export class PublicModule { }
