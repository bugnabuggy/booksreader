import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { 
  ProfileComponent, 
  BecomeAnAuthorComponent 
} from './pages';


const routes: Routes = [
  { path: Endpoints.frontend.user.profile, component: ProfileComponent},
  { path: Endpoints.frontend.user.becomeAnAuthor, component: BecomeAnAuthorComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
