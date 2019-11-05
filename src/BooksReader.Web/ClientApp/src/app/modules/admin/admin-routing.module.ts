import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from '@br/config';
import { 
    AllUsersComponent,
    AdminDashboardComponent, 
    AllDomainsComponent, 
    AllBooksComponent} from './pages';



const routes: Routes = [
  { path:Endpoints.frontend.admin.allBooks, component: AllBooksComponent },
  { path:Endpoints.frontend.admin.allUsers, component: AllUsersComponent },
  { path:Endpoints.frontend.admin.allDomains, component: AllDomainsComponent },
  { path:Endpoints.frontend.admin.dashboard, component: AdminDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
