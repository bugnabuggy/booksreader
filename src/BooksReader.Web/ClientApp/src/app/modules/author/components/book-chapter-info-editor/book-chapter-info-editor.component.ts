import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { BookChapter, BookChapterRequest } from '@br/core/models';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BookChapterEditingService, NotificationService } from '@br/core/services';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-book-chapter-info-editor',
  templateUrl: './book-chapter-info-editor.component.html',
  styleUrls: ['./book-chapter-info-editor.component.scss']
})
export class BookChapterInfoEditorComponent implements OnInit, OnChanges {

  @Input() chapter: BookChapter;
  @Output() delete = new EventEmitter<BookChapter>();
  @Output() edit = new EventEmitter<BookChapter>();
  
  isUiBlocked: boolean = false;
  chapterForm: FormGroup = this.fb.group({
    isPublished: ''
  });

  constructor(
    private fb: FormBuilder,
    private chapterEditingSvc: BookChapterEditingService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
  }

  ngOnChanges() {
    this.chapterForm = this.fb.group({
      isPublished: [this.chapter.isPublished],
    });
  }

  save() {
    let data = this.chapterForm.value;

    let request = {
      title: this.chapter.title,
      id: this.chapter.id,
      content: this.chapter.content,
      description: this.chapter.description,
      
      isPublished: data.isPublished,
    } as BookChapterRequest;

    this.chapterEditingSvc.save(this.chapter.bookId, request)
    .subscribe( val => {
      this.notifications.showSuccess(SiteMessages.author.chapters.edited);
      this.chapter = val.data;
    }, err => {
      this.notifications.showError(err);
    });

  }
}
