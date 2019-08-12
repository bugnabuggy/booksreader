import { Component, OnInit, Input } from '@angular/core';
import { Book, BookChapter } from '@br/core/models';
import { BreakpointObserver, Breakpoints} from '@angular/cdk/layout';

@Component({
  selector: 'app-book-content-editor',
  templateUrl: './book-content-editor.component.html',
  styleUrls: ['./book-content-editor.component.scss']
})
export class BookContentEditorComponent implements OnInit {

  sidebarMode = 'side';
  sidebarIsOpened = true;

  @Input() book: Book;
  @Input() chapters: BookChapter[];

  constructor(
    private breakpointObserver: BreakpointObserver,

    ) { }

  ngOnInit() {
    this.breakpointObserver.observe([
      Breakpoints.Small,
      Breakpoints.Large
    ]).subscribe(val=>{
      let isSmall = this.breakpointObserver.isMatched('(max-width: 599px)');
      this.sidebarMode = isSmall 
        ? 'over'
        : 'side';

      this.sidebarIsOpened = !isSmall;
    });
  }
}
