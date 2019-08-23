import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Book } from '@br/core/models';
import { PersonalPage } from '@br/core/models/api-contracts/entities';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookEditingService } from '@br/core/services';

@Component({
  selector: 'app-book-info-edit',
  templateUrl: './book-info-edit.component.html',
  styleUrls: ['./book-info-edit.component.scss']
})
export class BookInfoEditComponent implements OnInit, OnChanges {
  
  isUiBlocked: boolean;

  bookForm: FormGroup;

  @Input() book: Book;
  @Input() bookPage: PersonalPage;

  constructor(
    private fb: FormBuilder,
    private bookEditingSvc: BookEditingService
  ) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if(!this.book){
      this.bookForm = this.fb.group({
        title: [],
        author: [],
        description: [],
        isPublished: [],

        publicPage: this.fb.group({
          domain: [],
          urlPath: [],
          content: []
        })
      });
      return;
    }
    
    this.bookForm = this.fb.group({
      title: [ this.book.title, [Validators.required]],
      author:[ this.book.author ],
      description: [this.book.description],
      isPublished: [this.book.isPublished],

      publicPage: this.fb.group({
        domain: [this.bookPage.domain],
        urlPath: [this.bookPage.urlPath],
        content: [this.bookPage.content]
      })
    });
  }

  save(){
    console.log(`save ${this.bookForm.value}`);
    
    //this.bookEditingSvc.update();
  }

}
