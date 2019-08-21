import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BookChapter } from '@br/core/models';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BookChapterEditingService, NotificationService } from '@br/core/services';

@Component({
  selector: 'app-book-chapter-info-editor',
  templateUrl: './book-chapter-info-editor.component.html',
  styleUrls: ['./book-chapter-info-editor.component.scss']
})
export class BookChapterInfoEditorComponent implements OnInit {

  @Input() chapter: BookChapter;
  @Output()delete = new EventEmitter<BookChapter>();
  @Output()edit = new EventEmitter<BookChapter>();

  chapterForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private chapterEditingSvc: BookChapterEditingService,
    private notifications: NotificationService
    ) { 
      this.chapterForm = this.fb.group({
        isPublished: [false]
      });
  }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe(val=>{
      this.chapter = val;
      this.chapterForm.patchValue({
        isPublished: val.isPublished
      });
    })
  }

  save() {
    
    this.chapterEditingSvc.save(this.chapterForm.value).subscribe(val=>{

    }, err=>{
      this.notifications.showError(err.error);
    });
  }

}
