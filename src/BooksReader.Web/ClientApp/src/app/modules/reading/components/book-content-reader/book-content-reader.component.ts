import { Component, OnInit, Input } from '@angular/core';
import { BookReadingService } from '@br/core/services';
import { BookChapter } from '@br/core/models';

@Component({
  selector: 'app-book-content-reader',
  templateUrl: './book-content-reader.component.html',
  styleUrls: ['./book-content-reader.component.scss']
})
export class BookContentReaderComponent implements OnInit {
  
  @Input() chapter: BookChapter;

  constructor(
  ) { }

  ngOnInit() {
  }

}
