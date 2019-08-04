import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { 
    AuthorDashboardPageComponent,
    AuthorProfilePageComponent, 
    AuthorContainerComponent
  } from './pages';

const routes: Routes = [
  { path: '' , component: AuthorContainerComponent, children: [
    { path:Endpoints.forntend.author.dashboard, component: AuthorDashboardPageComponent },
    { path:Endpoints.forntend.author.profile, component: AuthorProfilePageComponent } 
  ]},
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
