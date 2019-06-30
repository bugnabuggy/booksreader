import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { LoginComponent, RegistrationComponent, ForceLogoutComponent } from './pages';
import { SharedModule } from '@br/shared/shared.module';
import { HeaderComponent } from './components/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { MainComponent } from './pages/main/main.component';

@NgModule({
  declarations: [
    HeaderComponent,

    LoginComponent,
    RegistrationComponent,
    MainComponent,
    ForceLogoutComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    TranslateModule.forChild(),
    PublicRoutingModule
  ],
  exports:[
    HeaderComponent,

    LoginComponent,
    RegistrationComponent,
    ForceLogoutComponent,
  ]
})
export class PublicModule { }
