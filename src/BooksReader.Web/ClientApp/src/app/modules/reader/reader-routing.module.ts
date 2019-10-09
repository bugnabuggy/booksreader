import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '@br/core/guards';
import { Endpoints } from '@br/config';

import { 
  ReaderDashboardComponent,
  ReaderProfileComponent } from './pages';

const routes: Routes = [
  { path: Endpoints.frontend.reader.dashboard, component: ReaderDashboardComponent, canActivate:[AuthGuard]},
  { path: Endpoints.frontend.reader.profile, component: ReaderProfileComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReaderRoutingModule { }
