import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReaderRoutingModule } from './reader-routing.module';
import { ReaderDashboardComponent } from './pages';
import { ReaderProfileComponent } from './pages/reader-profile/reader-profile.component';

@NgModule({
  declarations: [
    ReaderDashboardComponent,
    ReaderProfileComponent
  ],
  imports: [
    CommonModule,
    ReaderRoutingModule
  ]
})
export class ReaderModule { }
