import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorRoutingModule } from './author-routing.module';
import { AuthorDashboardComponent } from './pages/author-dashboard/author-dashboard.component';
import { AuthorContainerComponent } from './pages/author-container/author-container.component';
import { AuthorProfileComponent } from './pages/author-profile/author-profile.component';
import { SharedModule } from '@br/shared/shared.module';
import { AuthorBooksListComponent } from './components/author-books-list/author-books-list.component';
import { AddBookDialogComponent } from './components/add-book-dialog/add-book-dialog.component';
import { AuthorBooksListItemComponent } from './components/author-books-list-item/author-books-list-item.component';
import { BookEditingComponent } from './pages/book-editing/book-editing.component';
import { BookInfoEditComponent } from './pages/book-info-edit/book-info-edit.component';
import { TranslateModule } from '@ngx-translate/core';
import { BookPropertiesComponent } from './components/book-properties/book-properties.component';


@NgModule({
  declarations: [
    AuthorDashboardComponent,
    AuthorContainerComponent,
    AuthorProfileComponent, 
    AuthorBooksListComponent,
    AddBookDialogComponent,
    AuthorBooksListItemComponent,
    BookEditingComponent,
    BookInfoEditComponent,
    BookPropertiesComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AuthorRoutingModule,
    TranslateModule.forChild()
  ],
  entryComponents: [
    AddBookDialogComponent
  ]
})
export class AuthorModule { }
