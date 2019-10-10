import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { AllUsersComponent } from './pages/all-users/all-users.component';
import { SharedModule } from '@br/shared/shared.module';


@NgModule({
  declarations: [AdminDashboardComponent, AllUsersComponent],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
