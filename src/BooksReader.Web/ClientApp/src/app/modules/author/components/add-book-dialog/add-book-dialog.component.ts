import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book } from '@br/core/models';

@Component({
  selector: 'app-add-book-dialog',
  templateUrl: './add-book-dialog.component.html',
  styleUrls: ['./add-book-dialog.component.scss']
})
export class AddBookDialogComponent implements OnInit {

  bookName: string = '';

  constructor(
    public dialogRef: MatDialogRef<AddBookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Book
  ) { }

  ngOnInit() {
    this.bookName = this.data.title;
  }

  close() {
    this.dialogRef.close();
  }

  add() {
    this.dialogRef.close(this.bookName);
  }
}
