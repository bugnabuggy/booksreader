import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { 
  LoginComponent,
  RegistrationComponent, 
  ForceLogoutComponent, 
  MainComponent 
  } from './pages';
import { Endpoints } from '@br/config';


const routes: Routes = [
  { path: Endpoints.frontend.main, component: MainComponent },
  { path: Endpoints.frontend.authorization, component: LoginComponent },
  { path: Endpoints.frontend.registration, component: RegistrationComponent },
  { path: Endpoints.frontend.user.forceLogout, component: ForceLogoutComponent },

  { 
    path: '',
    redirectTo: Endpoints.frontend.main,
    pathMatch: 'full'  
  },

  // for any public page route show main page
  {
    path: '**',
    component: MainComponent
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
