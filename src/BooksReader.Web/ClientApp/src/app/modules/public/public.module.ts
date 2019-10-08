import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@br/shared/shared.module';
import { LoginComponent } from './pages/login/login.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { ForceLogoutComponent } from './pages/force-logout/force-logout.component';
import { MainComponent } from './pages/main/main.component';


@NgModule({
  declarations: [
    HeaderComponent,
    LoginComponent,
    RegistrationComponent,
    ForceLogoutComponent,
    MainComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    TranslateModule.forChild(),
    SharedModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class PublicModule { }
