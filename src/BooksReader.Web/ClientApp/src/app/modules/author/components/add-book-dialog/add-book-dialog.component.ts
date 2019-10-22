import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book, BasicBookInfo } from '@br/core/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '@br/core/services';

@Component({
  selector: 'app-add-book-dialog',
  templateUrl: './add-book-dialog.component.html',
  styleUrls: ['./add-book-dialog.component.scss']
})
export class AddBookDialogComponent implements OnInit {
  bookBasicForm =  new FormGroup({});

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddBookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Book,
    private notifications: NotificationService
  ) { 
  }

  ngOnInit() {
    this.bookBasicForm = this.fb.group({
      id: [this.data.id],
      title: [this.data.title, [Validators.required]],
      author: [this.data.author, [Validators.required]],
      picture: [this.data.picture]
    });
  }

  close() {
    this.dialogRef.close();
  }

  add() {
    this.dialogRef.close(this.bookBasicForm.value);
  }

  showErr(message) {
    this.notifications.showError(message);
  }

  deleteImage() {
    this.bookBasicForm.patchValue({picture:''});
  }
}
