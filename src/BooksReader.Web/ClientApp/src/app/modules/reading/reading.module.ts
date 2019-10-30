import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { 
  BookReaderComponent, 
  BookContentReaderComponent,
  BookChaptersListComponent,
} from './components';
import { MaterialModule } from '@br/material/material.module';
import { BookChapterInfoComponent } from './components/book-chapter-info/book-chapter-info.component';



@NgModule({
  declarations: [
    BookReaderComponent,
    BookContentReaderComponent,
    BookChaptersListComponent,
    BookChapterInfoComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    BookReaderComponent
  ]
})
export class ReadingModule { }
