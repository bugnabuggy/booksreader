import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { BookChapter } from '@br/core/models';
import { BookChapterEditingService, BookReadingService } from '@br/core/services';

@Component({
  selector: 'app-book-chapters-list',
  templateUrl: './book-chapters-list.component.html',
  styleUrls: ['./book-chapters-list.component.scss']
})
export class BookChaptersListComponent implements OnInit {

  @Input() bookId: string;
  @Input() chapters: BookChapter[] = [];

  selectedChapterIndex: number = -1;

  constructor(
    private readingSvc: BookReadingService
  ) { }

  ngOnInit() {
    this.readingSvc.activeChapter.subscribe(val=>{
      this.selectedChapterIndex = this.chapters 
      ? this.chapters.findIndex(x=>x.id == (val && val.id))
      : -1;
    });
  }

  select(chapter: BookChapter) {
    this.readingSvc.activeChapter.next(chapter);
  }

  dec() {

  }

  inc() {
    
  }
}
