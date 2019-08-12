import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BookChapter, BookChapterRequest, OperationResult } from '@br/core/models';
import { BookChapterEditingService, NotificationService } from '@br/core/services';
import { SiteConstants, StringConstants } from '@br/config';
import { AddStringDialogComponent } from '../add-string-dialog/add-string-dialog.component';

import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-book-chapters-editor',
  templateUrl: './book-chapters-editor.component.html',
  styleUrls: ['./book-chapters-editor.component.scss']
})
export class BookChaptersEditorComponent implements OnInit {

  @Input() chapters: BookChapter[];
  @Input() bookId: string;

  chapterForm: FormGroup;


  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    private bookChapterEditingSvc: BookChapterEditingService,
    private notificationsSvc: NotificationService
  ) { 
    this.chapterForm = this.fb.group({

    });
  }

  ngOnInit() {
  }

  add() {
    const dialogRef = this.dialog.open(AddStringDialogComponent, {
      minHeight: "50%",
      data: {
        caption: 'Chapter name',
        title: 'Add a chapter'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        let request = {
          title: result
        } as BookChapterRequest;
        debugger;
        this.bookChapterEditingSvc.createOrUpdate(this.bookId, request)
        .subscribe((val: OperationResult<BookChapter>)=>{
          this.notificationsSvc.showSuccess(StringConstants.chapters.added);
          this.chapters.push(val.data);
        });
      }
    });
  }

  drop(event: CdkDragDrop<BookChapter[]>) {
    
    moveItemInArray(this.chapters, event.previousIndex, event.currentIndex);
    let order = this.chapters.map( (x, i)=> {
      return { id: x.id, number: i }
    });
    console.log(order);
  }

}
