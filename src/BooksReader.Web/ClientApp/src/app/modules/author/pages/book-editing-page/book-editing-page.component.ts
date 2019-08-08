import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book, BookChapter } from '@br/core/models';
import { BookEditingService, NotificationService } from '@br/core/services';
import { PersonalPage } from '@br/core/models/api-contracts/entities';
import { Endpoints, StringConstants } from '@br/config';

@Component({
  selector: 'app-book-editing-page',
  templateUrl: './book-editing-page.component.html',
  styleUrls: ['./book-editing-page.component.scss']
})
export class BookEditingPageComponent implements OnInit {

  book: Book;
  bookPage: PersonalPage;
  chapters: BookChapter[];

  tabIndex:number = 0;

  tabs = ['edit', 'props'];


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookEditingSvc: BookEditingService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(x => {
      let bookId = x.id;
      
      let index = this.tabs.findIndex(y => y == x.tab);
      this.tabIndex = index > 0
            ? index
            : 0;
            
      if (bookId && bookId != (this.book && this.book.id)) {
        this.bookEditingSvc.getFull(bookId).subscribe(y => {
          console.log(y);
          if (y.success) {
            this.book = y.data.book;
            this.chapters = y.data.chapters;
            this.bookPage = y.data.bookPage;
          }
        }, err => {
          this.notifications.showError(err.message || StringConstants.errors.anyError);
        });
      }
    });
  }

  changeTab(index: number) {
    let url = Endpoints.forntend.author.bookUrl
      .replace(':id',this.book.id)
      .replace(':tab',this.tabs[index]);

    this.router.navigateByUrl(url);
  }
}
