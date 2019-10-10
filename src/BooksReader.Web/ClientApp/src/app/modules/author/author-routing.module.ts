import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { AuthorContainerComponent, AuthorDashboardComponent, AuthorProfileComponent } from './pages';


const routes: Routes = [
  { path: '' , component: AuthorContainerComponent, children: [
    { path:Endpoints.frontend.author.dashboard, component: AuthorDashboardComponent },
    { path:Endpoints.frontend.author.profile, component: AuthorProfileComponent },
    // { path:Endpoints.frontend.author.book, component: BookEditingPageComponent },
    // { path:Endpoints.frontend.author.book.replace('/:tab',''), component: BookEditingPageComponent } 
  ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorRoutingModule { }
