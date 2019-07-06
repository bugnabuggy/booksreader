import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { ProfileComponent } from './pages';
import { BecomeAnAuthorPageComponent } from './pages/become-an-author-page/become-an-author-page.component';

const routes: Routes = [
  { path: Endpoints.forntend.user.profile, component: ProfileComponent},
  { path: Endpoints.forntend.user.becomeAnAuthor, component: BecomeAnAuthorPageComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
