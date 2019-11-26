import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationService, BookReadingService } from '@br/core/services';
import { Book, BookChapter, GeneralBook, ChapterReadingDto } from '@br/core/models';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-read-book',
  templateUrl: './read-book.component.html',
  styleUrls: ['./read-book.component.scss']
})
export class ReadBookComponent implements OnInit, OnDestroy {

  bookId: string;
  book: GeneralBook;
  chapters: ChapterReadingDto[];

  private routeSub: Subscription;
  private bookSub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private bookReadingSvc: BookReadingService,
    
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.bookSub = this.bookReadingSvc.activeBook.subscribe(val => {
      if(val){
        this.book = val.book;
        this.chapters = val.chapters;  
      } else {
        this.book = null;
        this.chapters = [];
      }
    });
    
    this.bookReadingSvc.clearState();

    this.routeSub = this.route.params.subscribe(x => {
      this.bookId = x.bookId;
      if(this.bookId) {
        this.bookReadingSvc.getBook(this.bookId)
        .subscribe(val=>{
          console.log(val);
          if(val.success) {
            this.bookReadingSvc.activeBook.next(val.data);
          }          
        }, err => {
          this.notifications.showError(err);
        })
      }
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
    this.bookSub.unsubscribe();
  }

}
