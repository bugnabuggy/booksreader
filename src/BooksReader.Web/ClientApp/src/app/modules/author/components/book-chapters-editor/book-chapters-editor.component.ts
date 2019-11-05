import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { BookChapter, BookChapterRequest, OperationResult, ChapterReorderRequest } from '@br/core/models';
import { MatDialog } from '@angular/material/dialog';
import { BookChapterEditingService, NotificationService } from '@br/core/services';
import { AddStringDialogComponent, ConfirmationDialogComponent } from '@br/controls/dialogs';
import { SiteMessages } from '@br/config/site-messages';
import { finalize } from 'rxjs/operators';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ConfirmationType, ConfirmationResult } from '@br/core/enums';
import { DialogModel } from '@br/core/models/site/dialog.model';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-book-chapters-editor',
  templateUrl: './book-chapters-editor.component.html',
  styleUrls: ['./book-chapters-editor.component.scss']
})
export class BookChaptersEditorComponent implements OnInit, OnChanges {

  @Input() chapters: BookChapter[];
  @Input() bookId: string;

  selectedChapterIndex: number = -1;
  uiIsBlocked = false;

  constructor(
    private dialog: MatDialog,
    private bookChapterEditingSvc: BookChapterEditingService,
    private notifications: NotificationService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.bookChapterEditingSvc.activeChapter.subscribe(val=>{
      this.selectedChapterIndex = this.chapters
      ? this.chapters.findIndex(x=>x.id == (val && val.id))
      : -1;
    });
  }


  ngOnChanges() {
    if (this.chapters && this.chapters.length > 0) {
      this.selectedChapterIndex = 0;
      this.bookChapterEditingSvc.activeChapter.next(this.chapters[0]);
    }
  }

  edit(chapter?: BookChapter) {
    const dialogRef = this.dialog.open(AddStringDialogComponent, {
      minHeight: "50%",
      minWidth: "50%",
      data: {
        text: SiteMessages.author.chapters.chapterName,
        title: chapter
        ? SiteMessages.author.chapters.edit
        : SiteMessages.author.chapters.add,
        value: chapter && chapter.title || ''
      } as DialogModel
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let request = {
          id: chapter && chapter.id || null,
          isPublished: chapter && chapter.isPublished || false,
          title: result
        } as BookChapterRequest;
        this.uiIsBlocked = true;
        this.bookChapterEditingSvc.save(this.bookId, request)
          .pipe(finalize(() => {
            this.uiIsBlocked = false;
          }))
          .subscribe((val: OperationResult<BookChapter>) => {
            let msg = chapter
                ? SiteMessages.author.chapters.edited
                : SiteMessages.author.chapters.added;

            this.notifications.showSuccess(msg);
            
            if(chapter) {
              let index = this.chapters.findIndex(x=>x.id == val.data.id);
              this.chapters[index] = val.data;
              this.bookChapterEditingSvc.activeChapter.next(val.data);

            } else {
              this.chapters.push(val.data);
            }
            
          }, err => {
            this.notifications.showError(err);
          });
      }
    });
  }

  drop(event: CdkDragDrop<BookChapter[]>) {

    moveItemInArray(this.chapters, event.previousIndex, event.currentIndex);
    this.selectedChapterIndex = event.currentIndex;

    let order = this.chapters.map((x, i) => {
      return { id: x.id, number: i } as ChapterReorderRequest
    });
    this.bookChapterEditingSvc.reorder(this.bookId, order).subscribe(val => {

    }, err => {
      this.notifications.showError(err.data || err.response || err.message);
    });

    this.bookChapterEditingSvc.activeChapter.next(this.chapters[event.currentIndex]);
  }

  select(chapter) {
    this.selectedChapterIndex = this.chapters.findIndex(x => x == chapter);
    this.bookChapterEditingSvc.activeChapter.next(chapter);
  }

  inc() {
    debugger;
    this.selectedChapterIndex = this.chapters.length >= this.selectedChapterIndex + 1
      ? 0
      : this.selectedChapterIndex++;
  }

  dec() {
    this.selectedChapterIndex = this.selectedChapterIndex < 1
      ? this.chapters.length
      : this.selectedChapterIndex--;
  }

  delete(chapter: BookChapter) {

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        title:'Please confirm',
        text: this.translate.instant(SiteMessages.author.chapters.deleteQuestion, {title: chapter.title}),
        type: ConfirmationType.yesNo
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == ConfirmationResult.yes) {
       this.bookChapterEditingSvc.delete(this.bookId, chapter.id)       
       .subscribe(val=>{
          this.chapters.splice(this.selectedChapterIndex, 1);  

          this.selectedChapterIndex = this.chapters.length <= this.selectedChapterIndex 
            ? this.chapters.length - 1
            : this.selectedChapterIndex;

          let selectedChapter = this.chapters[this.selectedChapterIndex];
          this.bookChapterEditingSvc.activeChapter.next(selectedChapter);

       }, err=>{
         this.notifications.showError(err);
       }); 
      }
    });
  }
}
