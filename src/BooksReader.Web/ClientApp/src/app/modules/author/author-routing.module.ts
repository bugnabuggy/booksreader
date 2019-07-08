import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { 
    AuthorDashboardPageComponent,
    AuthorProfilePageComponent 
  } from './pages';

const routes: Routes = [
  { path:Endpoints.forntend.author.dashboard, component: AuthorDashboardPageComponent },
  { path:Endpoints.forntend.author.profile, component: AuthorProfilePageComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
