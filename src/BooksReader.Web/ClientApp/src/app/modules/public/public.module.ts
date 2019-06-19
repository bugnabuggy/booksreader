import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { LoginComponent, RegistrationComponent } from './pages';
import { SharedModule } from '@br/shared/shared.module';

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PublicRoutingModule
  ],
  exports:[
    LoginComponent,
    RegistrationComponent
  ]
})
export class PublicModule { }
