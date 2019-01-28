import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SecurityService } from './services';

import {
  DashboardComponent,
  LoginComponent,
  RegistrationComponent,
  ForceLogoutComponent,
  AdminDashboardComponent,
  AuthorDashboardComponent
} from './pages';


const routes: Routes = [
  { path: '', redirectTo: '/authorize', pathMatch: 'full' },
  { path: 'logout', component: ForceLogoutComponent },
  { path: 'authorize', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [SecurityService] },
  { path: 'admin/dashboard', component: AdminDashboardComponent, canActivate: [SecurityService] },
  { path: 'author/dashboard', component: AuthorDashboardComponent, canActivate: [SecurityService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
