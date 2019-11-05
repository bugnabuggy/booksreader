import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminDashboardComponent } from './pages/admin-dashboard/admin-dashboard.component';
import { AllUsersComponent } from './pages/all-users/all-users.component';
import { SharedModule } from '@br/shared/shared.module';
import { AllDomainsComponent } from './pages/all-domains/all-domains.component';
import { TranslateModule } from '@ngx-translate/core';
import { AllBooksComponent } from './pages/all-books/all-books.component';


@NgModule({
  declarations: [AdminDashboardComponent, AllUsersComponent, AllDomainsComponent, AllBooksComponent],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    TranslateModule.forChild()
  ]
})
export class AdminModule { }
