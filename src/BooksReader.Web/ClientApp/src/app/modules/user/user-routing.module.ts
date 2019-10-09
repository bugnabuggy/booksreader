import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { ProfileComponent } from './pages';


const routes: Routes = [
  { path: Endpoints.frontend.user.profile, component: ProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
