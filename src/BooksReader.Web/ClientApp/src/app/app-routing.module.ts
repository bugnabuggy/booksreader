import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '@br/core/guards/auth-guard';
import { MainComponent } from '@br/public/pages';
import { Endpoints } from './config';



const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: 'main', component: MainComponent },
  
  {    path: Endpoints.areas.user, canLoad: [AuthGuard], loadChildren: './modules/user/user.module#UserModule'  },
  {    path: Endpoints.areas.reader, canLoad: [AuthGuard], loadChildren: './modules/reader/reader.module#ReaderModule'  },
  {    path: Endpoints.areas.author, canLoad: [AuthGuard], loadChildren: './modules/author/author.module#AuthorModule'  },
  {    path: Endpoints.areas.admin, canLoad: [AuthGuard], loadChildren: './modules/admin/admin.module#AdminModule'  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
