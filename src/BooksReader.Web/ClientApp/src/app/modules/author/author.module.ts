import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditorModule } from '@tinymce/tinymce-angular';



import { AuthorRoutingModule } from './author-routing.module';
import { AuthorDashboardPageComponent } from './pages/author-dashboard-page/author-dashboard-page.component';
import { AuthorProfilePageComponent } from './pages/author-profile-page/author-profile-page.component';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { AddBookDialogComponent } from './components/add-book-dialog/add-book-dialog.component';
import { AuthorContainerComponent } from './pages/author-container/author-container.component';
import { BookEditingPageComponent } from './pages/book-editing-page/book-editing-page.component';
import { BookInfoEditComponent } from './components/book-info-edit/book-info-edit.component';
import { BookContentEditorComponent } from './components/book-content-editor/book-content-editor.component';
import { BookChaptersEditorComponent } from './components/book-chapters-editor/book-chapters-editor.component';
import { BookChapterInfoEditorComponent } from './components/book-chapter-info-editor/book-chapter-info-editor.component';
import { BookContentInnerEditorComponent } from './components/book-content-inner-editor/book-content-inner-editor.component';
import { AddStringDialogComponent } from './components/add-string-dialog/add-string-dialog.component';
import { BookReaderPreviewComponent } from './components/book-reader-preview/book-reader-preview.component';

@NgModule({
  declarations: [
    AuthorDashboardPageComponent,
    AuthorProfilePageComponent,
    AddBookDialogComponent,
    AuthorContainerComponent,
    BookEditingPageComponent,
    BookInfoEditComponent,
    BookContentEditorComponent,
    BookChaptersEditorComponent,
    BookChapterInfoEditorComponent,
    BookContentInnerEditorComponent,
    AddStringDialogComponent,
    BookReaderPreviewComponent],
  imports: [
    CommonModule,
    AuthorRoutingModule,
    SharedModule,
    TranslateModule.forChild(),
    EditorModule 
  ],
  entryComponents:[
    AddBookDialogComponent,
    AddStringDialogComponent
  ]

})
export class AuthorModule { }
