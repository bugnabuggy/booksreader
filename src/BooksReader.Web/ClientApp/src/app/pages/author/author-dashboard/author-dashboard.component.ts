import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { BookEditDialogComponent } from '../../../components';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.scss']
})
export class AuthorDashboardComponent implements OnInit {
  books: any[] = [{ name: '42' }, { name: '1984' }];

  constructor(
    public dialog: MatDialog
  ) { }

  ngOnInit() {
  }

  add() {
    const dialogRef = this.dialog.open(BookEditDialogComponent, {
      width: '250px',
      data: {  }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
