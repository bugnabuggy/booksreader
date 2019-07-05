import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { AllUsersPageComponent } from './pages';

const routes: Routes = [
  {path:Endpoints.forntend.admin.allUsers, component:AllUsersPageComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
