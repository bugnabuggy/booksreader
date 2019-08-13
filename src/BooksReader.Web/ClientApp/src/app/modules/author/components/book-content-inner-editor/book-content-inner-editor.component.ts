import { Component, OnInit } from '@angular/core';
import { BookChapterEditingService } from '@br/core/services';
import { BookChapter } from '@br/core/models';

@Component({
  selector: 'app-book-content-inner-editor',
  templateUrl: './book-content-inner-editor.component.html',
  styleUrls: ['./book-content-inner-editor.component.scss']
})
export class BookContentInnerEditorComponent implements OnInit {

  bookChapter: BookChapter = null;

  constructor(
    private chapterEditingSvc: BookChapterEditingService   
  ) { }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe((chapter: BookChapter)=>{
      this.bookChapter = chapter;
    })
  }

}
