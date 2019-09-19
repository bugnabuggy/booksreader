import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { BookChapter, BookChapterRequest, OperationResult, ChapterReorderRequest } from '@br/core/models';
import { BookChapterEditingService, NotificationService } from '@br/core/services';
import { SiteConstants, StringConstants } from '@br/config';
import { AddStringDialogComponent } from '../add-string-dialog/add-string-dialog.component';

import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import { ConfirmationDialogComponent } from '@br/shared/components';
import { ConfirmationType, ConfirmationResult } from '@br/core/enums';


@Component({
  selector: 'app-book-chapters-editor',
  templateUrl: './book-chapters-editor.component.html',
  styleUrls: ['./book-chapters-editor.component.scss']
})
export class BookChaptersEditorComponent implements OnInit, OnChanges{

  @Input() chapters: BookChapter[];
  @Input() bookId: string;

  selectedChapterIndex: number = -1; 
  

  constructor(    
    private dialog: MatDialog,
    private bookChapterEditingSvc: BookChapterEditingService,
    private notificationsSvc: NotificationService,
  ) { 
  }

  ngOnInit() {
  }

  ngOnChanges() {
    if(this.chapters && this.chapters.length > 0) {
      this.selectedChapterIndex = 0;
      this.bookChapterEditingSvc.activeChapter.next(this.chapters[0]);
    }
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
        let request = {
          title: result
        } as BookChapterRequest;
        this.bookChapterEditingSvc.createOrUpdate(this.bookId, request)
        .subscribe((val: OperationResult<BookChapter>)=>{
          this.notificationsSvc.showSuccess(StringConstants.chapters.added);
          this.chapters.push(val.data);
        });
      }
    });
  }

  edit() {
    let chapter:BookChapter = this.chapters[this.selectedChapterIndex];

    const dialogRef = this.dialog.open(AddStringDialogComponent, {
      minHeight: "50%",
      data: {
        name: chapter.title,
        caption: 'Chapter name',
        title: 'Edit chapter name'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let request = {
          title: result,
          id: chapter.id,
        } as BookChapterRequest;

        this.bookChapterEditingSvc.createOrUpdate(this.bookId, request)
        .subscribe((val: OperationResult<BookChapter>)=>{
          this.notificationsSvc.showSuccess(StringConstants.chapters.edited);
          this.chapters.find(x=>x.id == val.data.id).title = val.data.title;
        });
      }
    });
  }

  delete(){
    let chapter = this.chapters[this.selectedChapterIndex];

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        title:'Please confirm',
        question:`Are you sure that wnat to delete [${chapter.title}] chapter?`,
        type: ConfirmationType.yesNo
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      debugger;
      if (result == ConfirmationResult.yes) {
       this.bookChapterEditingSvc.delete(this.bookId, chapter.id)       
       .subscribe(val=>{
          this.chapters.splice(this.selectedChapterIndex, 1);  

          this.selectedChapterIndex = this.chapters.length <= this.selectedChapterIndex 
            ? this.chapters.length - 1
            : this.selectedChapterIndex;

       }, err=>{
         this.notificationsSvc.showError(err.response || err.message || StringConstants.chapters.deletionError);
       }); 
      }
    });
  }

  drop(event: CdkDragDrop<BookChapter[]>) {
    
    moveItemInArray(this.chapters, event.previousIndex, event.currentIndex);
    this.selectedChapterIndex = event.currentIndex;
    
    let order = this.chapters.map( (x, i)=> {
      return { id: x.id, number: i } as ChapterReorderRequest
    });
    this.bookChapterEditingSvc.reorder(this.bookId, order).subscribe(val=>{

    }, err => {
      this.notificationsSvc.showError(err.data || err.response || err.message);
    });

    this.bookChapterEditingSvc.activeChapter.next(this.chapters[event.currentIndex]);
  }

  select(chapter) {
    this.selectedChapterIndex = this.chapters.findIndex(x=>x == chapter);
    this.bookChapterEditingSvc.activeChapter.next(chapter);
  }

  inc(){
    this.selectedChapterIndex = this.chapters.length >= this.selectedChapterIndex + 1
    ? 0
    : this.selectedChapterIndex++;
  }

  dec(){
    this.selectedChapterIndex = this.selectedChapterIndex < 1
    ? this.chapters.length
    : this.selectedChapterIndex--;
  }
}
