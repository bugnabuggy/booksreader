import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Book } from '@br/core/models';
import { PersonalPage, BookPrice, TypeValue } from '@br/core/models/api-contracts/entities';
import { FormBuilder, FormGroup, Validators, FormArray, ValidatorFn, ValidationErrors } from '@angular/forms';
import { BookEditingService, ListsService, NotificationService } from '@br/core/services';
import { StringConstants, SiteConstants } from '@br/config';
import { toArray } from '@br/utilities/converters';

@Component({
  selector: 'app-book-info-edit',
  templateUrl: './book-info-edit.component.html',
  styleUrls: ['./book-info-edit.component.scss']
})
export class BookInfoEditComponent implements OnInit, OnChanges {

  isUiBlocked: boolean;

  bookForm: FormGroup;
  get prices() {
    return this.bookForm.get('bookPrices') as FormArray;
  }

  pricesVisible: boolean;
  currencies: TypeValue[];

  // bookPageForm: FormGroup;
  // bookPricesForm: FormArray;


  @Input() book: Book;
  @Input() bookPage: PersonalPage;
  @Input() bookPrices: BookPrice[];



  constructor(
    private fb: FormBuilder,
    private bookEditingSvc: BookEditingService,
    private listSvc: ListsService,
    private notifications: NotificationService
  ) {
    this.bookForm = this.fb.group({
      book: this.fb.group({
        title: ['111', Validators.required],
        author: ['', Validators.required],
        description: [],
        isPublished: [],
        isForSale: []
      }),

      bookPage: this.fb.group({
        domain: [],
        urlPath: [],
        content: []
      }),

      bookPrice: this.fb.group({
        price: [],
        currencyId: [],
        id: []
      })
    }, { validators: this.priceFilledValidator });
  }

  ngOnInit() {
    this.currencies = this.listSvc.get(SiteConstants.lists.сurrency).values;

    this.bookForm.valueChanges.subscribe(x => {
      this.pricesVisible = x.book.isForSale;
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (!this.book) { return }

    const bookPrice = this.bookPrices && this.bookPrices.length > 0
      ? { price: this.bookPrices[0].price, id: this.bookPrices[0].id, currencyId: this.bookPrices[0].currencyId } 
      : { price: '', id: '', currencyId: ''};
    

    this.bookForm.patchValue({
      book: {
        title: this.book.title,
        author: this.book.author,
        description: this.book.description,
        isPublished: this.book.isPublished,
        isForSale: this.book.isForSale
      },
      bookPrice: bookPrice
    });
  }

  save() {
    console.log(this.bookForm.value);
    
    // convert price to array

    let form = this.bookForm.value;
    form.prices  =  toArray<BookPrice>(form.bookPrice); 

    this.bookEditingSvc.editFull(this.book.id, form).subscribe( val => {
      this.notifications.showSuccess(StringConstants.books.edited)
    });
  }


  priceFilledValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
    const isForSale = control.get('book').get('isForSale');

    if (isForSale && isForSale.value) {
      const price = control.get('bookPrice').get('price');
      const currency = control.get('bookPrice').get('currencyId');
      
      let validations = {};
      
      if(!(price && (price.value > 0))) { validations['priceInvalid'] = true; }
      if(!(currency && (currency.value > 0))) {validations['currencyInvalid'] = true; }

      if(!Object.keys(validations).length){
        return null
      }

      return validations;
    }

    return null;
  };

}
