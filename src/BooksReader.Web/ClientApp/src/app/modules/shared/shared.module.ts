import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '@br/material/material.module';

import { TranslateModule } from '@ngx-translate/core';
import { BooksListComponent } from './components/books-list/books-list.component';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BooksListItemComponent } from './components/books-list-item/books-list-item.component';
import { BooksListAuthorItemComponent } from './components/books-list-author-item/books-list-author-item.component';
import { BooksListNoContentComponent } from './components/books-list-no-content/books-list-no-content.component';

@NgModule({
  declarations: [BooksListComponent, BooksListItemComponent, BooksListAuthorItemComponent, BooksListNoContentComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule.forChild(),
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    BooksListComponent,    
  ]
})
export class SharedModule { }
