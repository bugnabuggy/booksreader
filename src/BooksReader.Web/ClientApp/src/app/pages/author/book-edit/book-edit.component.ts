import { Component, OnInit } from '@angular/core';
import { Book, BookChapter, OperationResult } from '../../../models';
import { BookEditService } from '../../../services';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.scss']
})
export class BookEditComponent implements OnInit {
  book = { title: ''} as Book;
  capters: BookChapter[];

  constructor(
    private route: ActivatedRoute,
    public bookEditSvc: BookEditService
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.bookEditSvc.getBook(id).pipe(
      map((val: OperationResult<Book>) => { this.book = val.data; }),
      mergeMap(() => this.bookEditSvc.getChapters(id))
    ).subscribe(val => {
      console.log(val);
    });
  }

}
