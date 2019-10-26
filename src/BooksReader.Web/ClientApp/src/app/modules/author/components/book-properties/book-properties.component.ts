import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Book } from '@br/core/models';

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
    isPublished: false
  });

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
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
    });

    this.bookForm.valueChanges.subscribe(val => {
        this.valueChanged.emit(this.bookForm);      
    });
  }


  updateBook() {
    console.log(this.bookForm.valid);
  }
}
