import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { 
  BookReaderComponent, 
  BookContentReaderComponent,
  BookChaptersListComponent,
} from './components';
import { MaterialModule } from '@br/material/material.module';
import { BookChapterInfoComponent } from './components/book-chapter-info/book-chapter-info.component';
import { CoreModule } from '@br/core/core.module';
import { TranslateModule } from '@ngx-translate/core';
import { ReadingTopNavComponent } from './components/reading-top-nav/reading-top-nav.component';



@NgModule({
  declarations: [
    BookReaderComponent,
    BookContentReaderComponent,
    BookChaptersListComponent,
    BookChapterInfoComponent,
    ReadingTopNavComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    MaterialModule,
    TranslateModule.forChild()
  ],
  exports: [
    BookReaderComponent
  ]
})
export class ReadingModule { }
