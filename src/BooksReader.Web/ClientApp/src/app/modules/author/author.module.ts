import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorRoutingModule } from './author-routing.module';
import { AuthorDashboardComponent } from './pages/author-dashboard/author-dashboard.component';
import { AuthorContainerComponent } from './pages/author-container/author-container.component';
import { AuthorProfileComponent } from './pages/author-profile/author-profile.component';


@NgModule({
  declarations: [AuthorDashboardComponent, AuthorContainerComponent, AuthorProfileComponent],
  imports: [
    CommonModule,
    AuthorRoutingModule
  ]
})
export class AuthorModule { }
