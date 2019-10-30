import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditorModule } from '@tinymce/tinymce-angular';
import { TranslateModule } from '@ngx-translate/core';

import { SharedModule } from '@br/shared/shared.module';
import { ReadingModule } from '@br/reading/reading.module';

import { AuthorRoutingModule } from './author-routing.module';

import {
          AuthorDashboardComponent,
          AuthorContainerComponent,
          AuthorProfileComponent,
          BookEditingComponent, 
          BookInfoEditComponent, 
          BookContentEditorComponent
        } from './pages';

import { 
        AuthorBooksListComponent, 
        AddBookDialogComponent, 
        AuthorBooksListItemComponent, 
        BookPropertiesComponent, 
        BookChaptersEditorComponent,
        BookChapterInfoEditorComponent,
        BookContentInnerEditorComponent
      } from './components';



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
    BookPropertiesComponent,
    BookContentEditorComponent,
    BookChaptersEditorComponent,
    BookChapterInfoEditorComponent,
    BookContentInnerEditorComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AuthorRoutingModule,
    ReadingModule,
    TranslateModule.forChild(),
    EditorModule 
  ],
  entryComponents: [
    AddBookDialogComponent
  ]
})
export class AuthorModule { }
