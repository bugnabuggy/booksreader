import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddBookDialogComponent } from '../../components';

@Component({
  selector: 'app-author-dashboard-page',
  templateUrl: './author-dashboard-page.component.html',
  styleUrls: ['./author-dashboard-page.component.scss']
})
export class AuthorDashboardPageComponent implements OnInit {

  constructor(
    public dialog: MatDialog,
  ) { }

  ngOnInit() {
  }

  add() {
    const dialogRef = this.dialog.open(AddBookDialogComponent, {
      minHeight:"50%",
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }
}
