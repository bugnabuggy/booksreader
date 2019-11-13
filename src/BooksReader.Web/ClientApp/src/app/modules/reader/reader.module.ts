import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReaderRoutingModule } from './reader-routing.module';
import { ReaderProfileComponent } from './pages/reader-profile/reader-profile.component';
import { ReaderDashboardComponent } from './pages/reader-dashboard/reader-dashboard.component';
import { ReaderBooksListComponent } from './components/reader-books-list/reader-books-list.component';
import { ReaderBooksListItemComponent } from './components/reader-books-list-item/reader-books-list-item.component';

@NgModule({
  declarations: [
    ReaderProfileComponent, 
    ReaderDashboardComponent, ReaderBooksListComponent, ReaderBooksListItemComponent, 
    ],
  imports: [
    CommonModule,
    ReaderRoutingModule
  ]
})
export class ReaderModule { }
