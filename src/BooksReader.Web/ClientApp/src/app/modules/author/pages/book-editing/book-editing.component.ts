import { Component, OnInit } from '@angular/core';
import { BookEditingService, NotificationService } from '@br/core/services';
import { ActivatedRoute, Router } from '@angular/router';
import { Book, PublicPage, BookChapter, BookPrice } from '@br/core/models';
import { SiteMessages } from '@br/config/site-messages';
import { Endpoints } from '@br/config';
import { PublicPageType } from '@br/core/enums';

@Component({
  selector: 'app-book-editing',
  templateUrl: './book-editing.component.html',
  styleUrls: ['./book-editing.component.scss']
})
export class BookEditingComponent implements OnInit {

  book: Book = {
    id: 'no-book'
  } as Book;
  bookPage: PublicPage;
  bookPrices: BookPrice[];
  chapters: BookChapter[];


  bookId: string;

  tabIndex:number = 0;
  tabs = ['edit', 'props', 'preview'];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookEditingSvc: BookEditingService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(x => {
      let bookId = x.id;
      this.bookId = bookId;

      let index = this.tabs.findIndex(y => y == x.tab);
      this.tabIndex = index > 0
            ? index
            : 0;
            
      if (bookId && bookId != (this.book && this.book.id)) {
        this.bookEditingSvc.getFull(bookId).subscribe((y:any) => {
          if (y.success) {
            this.book = y.data.book;
            this.chapters = y.data.chapters;
            this.bookPage = y.data.page;
            this.bookPrices = y.data.prices;
          }
        }, err => {
          this.notifications.showError(err, SiteMessages.errors.anyError);
        });
      }
    });
  }
  
  changeTab(index: number) {
    let url = Endpoints.frontend.author.bookUrl
      .replace(':id',this.bookId)
      .replace(':tab',this.tabs[index]);

    this.router.navigateByUrl(url);
  }
}
