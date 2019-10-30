import { Component, OnInit, Input } from '@angular/core';
import { BookChapter } from '@br/core/models';
import { BookChapterEditingService } from '@br/core/services';

@Component({
  selector: 'app-book-chapters-list',
  templateUrl: './book-chapters-list.component.html',
  styleUrls: ['./book-chapters-list.component.scss']
})
export class BookChaptersListComponent implements OnInit {

  @Input() bookId: string;
  @Input() chapters: BookChapter[];

  selectedChapterIndex: number = -1;

  constructor(
    private chapterEditingSvc: BookChapterEditingService
  ) { }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe(val => { 
      this.selectedChapterIndex = this.chapters.findIndex(x=>x.id == val.id);
    })
  }

  select(chapter: BookChapter) {
    this.chapterEditingSvc.activeChapter.next(chapter);
  }

  dec() {

  }

  inc() {
    
  }
}
