import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@br/shared/shared.module';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    HeaderComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    TranslateModule.forChild(),
    SharedModule,
    RouterModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class PublicModule { }
