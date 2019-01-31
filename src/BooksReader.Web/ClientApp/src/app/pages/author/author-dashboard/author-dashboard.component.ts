import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { BookEditDialogComponent } from '../../../components';
import { AuthorBooksService } from '../../../services';
import { WebResult, Book, OperationResult } from '../../../models';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.scss']
})
export class AuthorDashboardComponent implements OnInit {
  books: Book[];

  constructor(
    public dialog: MatDialog,
    private authorBooksService: AuthorBooksService
  ) { }

  ngOnInit() {
     this.authorBooksService.getBooks()
     .subscribe( (val: WebResult<Book[]> ) => {
      this.books = val.data;
     });
  }

  add() {
    const dialogRef = this.dialog.open(BookEditDialogComponent, {
      data: {  }
    });

    dialogRef.afterClosed().subscribe( (result: Book) => {
      if (result) {
        this.authorBooksService.addBook(result)
          .subscribe( (val: OperationResult<Book>) => {
            console.log(val);
            if (val.success) {
              this.books.push(val.data);
            }
          });
      }
      console.log(result);
    });
  }

  edit() {

  }

  delete(bookId: string) {
    this.authorBooksService.deleteBook(bookId)
    .subscribe( (val: OperationResult<Book>) => {
      if (val.success) {
        const index = this.books.indexOf(val.data);
        this.books.splice(index, 1);
      }
    });
  }

}
