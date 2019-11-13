import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidatorFn } from '@angular/forms';
import { Book } from '@br/core/models';
import { NotificationService } from '@br/core/services';

@Component({
  selector: 'app-book-properties',
  templateUrl: './book-properties.component.html',
  styleUrls: ['./book-properties.component.scss']
})
export class BookPropertiesComponent implements OnInit, OnChanges{

  @Input() book: Book;
  @Output() valueChanged = new EventEmitter<any>();

  bookForm = this.fb.group({
    title: '',
    author: '',
    description: '',
    picture: '',
    isForSale: false,
    isPublished: false,
    subscriptionDurationDays: 1
  });

  constructor(
    private fb: FormBuilder,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
  }

  formValidator(g: FormGroup) {
    if( g.get('isForSale').value) {
      let err;
      let control = g.get('subscriptionDurationDays');
      err = Validators.min(1)(control);
      err = err || Validators.max(36500)(control);
      return err;
    }

    return null;
  }

  ngOnChanges() {
    this.bookForm = this.fb.group({
      id: [this.book.id],
      ownerId: [this.book.ownerId],

      title: [this.book.title, Validators.required],
      author: [this.book.author, Validators.required],
      description: [this.book.description, Validators.required],
      picture: [this.book.picture],
      isPublished: [this.book.isPublished],
      isForSale: [this.book.isForSale],
      subscriptionDurationDays: [this.book.subscriptionDurationDays]
    }, { validators: [this.formValidator]});

    this.bookForm.valueChanges.subscribe(val => {
        this.valueChanged.emit(this.bookForm);      
    });
  }


  updateBook() {
    console.log(this.bookForm.valid);
  }

  showErr(message) {
    this.notifications.showError(message);
  }
}
