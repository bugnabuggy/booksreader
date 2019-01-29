import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { BookEditDialogComponent } from '../../../components';
import { BooksService } from '../../../services';
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
    private booksService: BooksService
  ) { }

  ngOnInit() {
     this.booksService.getBooks()
     .subscribe( (val: WebResult<Book[]> ) => {
      this.books = val.data;
     });
  }

  add() {
    const dialogRef = this.dialog.open(BookEditDialogComponent, {
      width: '250px',
      data: {  }
    });

    dialogRef.afterClosed().subscribe( (result: Book) => {
      if (result) {
        this.booksService.addBook(result)
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

}
