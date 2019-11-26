import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReaderRoutingModule } from './reader-routing.module';
import { ReaderProfileComponent } from './pages/reader-profile/reader-profile.component';
import { ReaderDashboardComponent } from './pages/reader-dashboard/reader-dashboard.component';
import { ReaderBooksListComponent } from './components/reader-books-list/reader-books-list.component';
import { ReaderBooksListItemComponent } from './components/reader-books-list-item/reader-books-list-item.component';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@br/shared/shared.module';
import { ReadBookComponent } from './pages/read-book/read-book.component';
import { ReadingModule } from '@br/reading/reading.module';

@NgModule({
  declarations: [
    ReaderProfileComponent, 
    ReaderDashboardComponent, ReaderBooksListComponent, ReaderBooksListItemComponent, ReadBookComponent, 
    ],
  imports: [
    CommonModule,
    TranslateModule.forChild(),
    SharedModule,
    ReaderRoutingModule,
    ReadingModule
  ]
})
export class ReaderModule { }
