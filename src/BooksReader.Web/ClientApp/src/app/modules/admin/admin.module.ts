import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AllUsersPageComponent } from './pages/all-users-page/all-users-page.component';
import { MaterialModule } from '@br/material/material.module';
import { TranslateModule } from '@ngx-translate/core';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';

@NgModule({
  declarations: [AllUsersPageComponent, AdminDashboardComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    TranslateModule.forChild()
  ]
})
export class AdminModule { }
