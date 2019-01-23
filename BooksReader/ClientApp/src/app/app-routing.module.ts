import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SecurityService } from './services';

import { DashboardComponent,
         LoginComponent,
         RegistrationComponent } from './pages';

const routes: Routes = [
  { path: '', redirectTo: '/authorization', pathMatch: 'full' },
  // { path: 'dashboard', component: DashboardComponent, canActivate: [] },
  { path: 'authorization', component: LoginComponent },

  { path: 'registration', component: RegistrationComponent},
  { path: 'dashboard', component: DashboardComponent,  canActivate: [SecurityService]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
