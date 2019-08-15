import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BookChapter } from '@br/core/models';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-book-chapter-info-editor',
  templateUrl: './book-chapter-info-editor.component.html',
  styleUrls: ['./book-chapter-info-editor.component.scss']
})
export class BookChapterInfoEditorComponent implements OnInit {

  @Input()chapter: BookChapter;
  @Output()delete = new EventEmitter<BookChapter>();
  @Output()edit = new EventEmitter<BookChapter>();

  chapterForm: FormGroup;

  constructor(private fb: FormBuilder) { 
    this.chapterForm = this.fb.group({
    });
  }

  ngOnInit() {
  }

}
