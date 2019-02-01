import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SecurityService } from './services';

import {
  DashboardComponent,
  LoginComponent,
  RegistrationComponent,
  ForceLogoutComponent,
  AdminDashboardComponent,
  AuthorDashboardComponent,
  BookMarketComponent,
  BookEditComponent
} from './pages';


const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'logout', component: ForceLogoutComponent },
  { path: 'authorize', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [SecurityService] },
  { path: 'book-market', component: BookMarketComponent, canActivate: [SecurityService] },
  { path: 'admin/dashboard', component: AdminDashboardComponent, canActivate: [SecurityService] },
  { path: 'author/dashboard', component: AuthorDashboardComponent, canActivate: [SecurityService] },
  { path: 'author/book', component: BookEditComponent, canActivate: [SecurityService] },
  { path: 'author/book/:id', component: BookEditComponent, canActivate: [SecurityService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
