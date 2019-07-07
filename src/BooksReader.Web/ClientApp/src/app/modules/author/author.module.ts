import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorRoutingModule } from './author-routing.module';
import { AuthorDashboardPageComponent } from './pages/author-dashboard-page/author-dashboard-page.component';
import { AuthorProfilePageComponent } from './pages/author-profile-page/author-profile-page.component';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [AuthorDashboardPageComponent, AuthorProfilePageComponent],
  imports: [
    CommonModule,
    AuthorRoutingModule,
    SharedModule,
    TranslateModule.forChild()
  ]
})
export class AuthorModule { }
