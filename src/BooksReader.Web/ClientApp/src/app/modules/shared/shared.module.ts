import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '@br/material/material.module';

import { TranslateModule } from '@ngx-translate/core';
import { BooksListComponent } from './components/books-list/books-list.component';

@NgModule({
  declarations: [BooksListComponent],
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
    BooksListComponent
  ]
})
export class SharedModule { }
