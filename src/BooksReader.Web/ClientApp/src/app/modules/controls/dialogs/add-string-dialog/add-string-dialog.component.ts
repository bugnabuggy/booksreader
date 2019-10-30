import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogModel } from '@br/core/models/site/dialog.model';

@Component({
  selector: 'app-add-string-dialog',
  templateUrl: './add-string-dialog.component.html',
  styleUrls: ['./add-string-dialog.component.scss']
})
export class AddStringDialogComponent implements OnInit {

  caption = '';
  value = '';
  title = '';

  constructor(
    public dialogRef: MatDialogRef<AddStringDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogModel
  ) { }

  ngOnInit() {
    this.value = this.data.value;
    this.caption = this.data.text; 
    this.title  = this.data.title;
  }

  close() {
    this.dialogRef.close();
  }

  add() {
    this.dialogRef.close(this.value);
  }

}
