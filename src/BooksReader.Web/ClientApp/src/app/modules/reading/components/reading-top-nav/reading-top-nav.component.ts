import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { BookChapter } from '@br/core/models';

@Component({
  selector: 'app-reading-top-nav',
  templateUrl: './reading-top-nav.component.html',
  styleUrls: ['./reading-top-nav.component.scss']
})
export class ReadingTopNavComponent implements OnInit {

  @Input() chapter: BookChapter;
  @Output() toggleChaptes = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

}
