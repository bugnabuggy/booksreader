import { Component, OnInit, Input } from '@angular/core';
import { BookChapter } from '@br/core/models';

@Component({
  selector: 'app-book-chapter-info',
  templateUrl: './book-chapter-info.component.html',
  styleUrls: ['./book-chapter-info.component.scss']
})
export class BookChapterInfoComponent implements OnInit {

  @Input() chapter: BookChapter;

  constructor() { }

  ngOnInit() {
  }

}
