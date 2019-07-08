import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '@br/core/guards/auth-guard';
import { ReaderDashboardComponent } from './pages';
import { Endpoints } from '@br/config';

const routes: Routes = [
  { path:Endpoints.forntend.reader.dashboard, component: ReaderDashboardComponent, canActivate:[AuthGuard]},
  { path: Endpoints.forntend.reader.profile, component: ReaderDashboardComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReaderRoutingModule { }
