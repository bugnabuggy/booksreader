import { Component, OnInit, Input } from '@angular/core';
import { Book, PublicPage, BookPrice, UserDomain } from '@br/core/models';
import { PublicPageType } from '@br/core/enums';
import { BookEditingService, AuthorProfileService, NotificationService } from '@br/core/services';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup } from '@angular/forms';
import { SiteMessages } from '@br/config/site-messages';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-book-info-edit',
  templateUrl: './book-info-edit.component.html',
  styleUrls: ['./book-info-edit.component.scss']
})
export class BookInfoEditComponent implements OnInit {

  @Input() book: Book;
  @Input() page: PublicPage;
  @Input() prices: BookPrice[];

  PublicPageType = PublicPageType;

  domains: UserDomain[] = [];

  uiIsBlocked = false;
  isBookForSale = false;
  bookValid = false;
  bookForm: FormGroup;

  constructor(
    private bookEditingSvc: BookEditingService,
    private authorProfileSvc: AuthorProfileService,
    private notifications: NotificationService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.authorProfileSvc.domains$.subscribe(val => {
      this.domains = val;
    })
  }

  bookChanaged(bookForm: FormGroup) {
    this.isBookForSale = bookForm.value.isForSale;
    this.bookValid = bookForm.valid;
    this.bookForm = bookForm;
  }

  get canSave() {
    let pricesIsOk = (this.isBookForSale && this.prices.filter(x => x.id).length > 0)
      || !this.isBookForSale;

    return this.bookValid && pricesIsOk;

  }

  saveBook() {
    var book = this.bookForm.value as Book;
    
    this.uiIsBlocked = true;

    this.bookEditingSvc.editFull(book)
      .pipe(finalize(()=>{
        this.uiIsBlocked = false;
      }))
      .subscribe(val => {
        this.notifications.showSuccess(SiteMessages.author.books.edited);
      });
  }

}
