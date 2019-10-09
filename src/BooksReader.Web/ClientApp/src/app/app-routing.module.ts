import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Endpoints } from './config';
import { AuthGuard } from '@br/core/guards';

const routes: Routes = [

  {    path: Endpoints.areas.user, canLoad: [AuthGuard], loadChildren: () => import('./modules/user/user.module').then(mod => mod.UserModule) },
  {    path: Endpoints.areas.reader, canLoad: [AuthGuard], loadChildren: () => import('./modules/reader/reader.module').then(mod => mod.ReaderModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
