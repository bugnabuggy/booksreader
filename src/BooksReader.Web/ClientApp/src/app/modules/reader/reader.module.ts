import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReaderRoutingModule } from './reader-routing.module';
import { ReaderProfileComponent } from './pages/reader-profile/reader-profile.component';
import { ReaderDashboardComponent } from './pages/reader-dashboard/reader-dashboard.component';

@NgModule({
  declarations: [
    ReaderProfileComponent, 
    ReaderDashboardComponent, 
    ],
  imports: [
    CommonModule,
    ReaderRoutingModule
  ]
})
export class ReaderModule { }
