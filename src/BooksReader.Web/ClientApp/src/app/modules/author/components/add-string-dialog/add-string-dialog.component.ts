import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-string-dialog',
  templateUrl: './add-string-dialog.component.html',
  styleUrls: ['./add-string-dialog.component.scss']
})
export class AddStringDialogComponent implements OnInit {
  name: string = '';
  caption: string = '';
  title: string = '';

  constructor(
    public dialogRef: MatDialogRef<AddStringDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { 
      name: string; 
      caption: string; 
      title: string;
    } 
  ) { }

  ngOnInit() {
    this.name = this.data.name;
    this.caption = this.data.caption; 
    this.title  = this.data.title;
  }

  close() {
    this.dialogRef.close();
  }

  add() {
    this.dialogRef.close(this.name);
  }
}
