import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
  LoginComponent,
  RegistrationComponent,
  ForceLogoutComponent
} from './pages';


const routes: Routes = [
  { path: 'authorize', component: LoginComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'forceLogout', component: ForceLogoutComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
