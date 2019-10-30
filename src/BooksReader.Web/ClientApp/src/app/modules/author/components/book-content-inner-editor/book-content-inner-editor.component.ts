import { Component, OnInit, Input } from '@angular/core';
import { BookChapter } from '@br/core/models';
import { BookChapterEditingService } from '@br/core/services';

@Component({
  selector: 'app-book-content-inner-editor',
  templateUrl: './book-content-inner-editor.component.html',
  styleUrls: ['./book-content-inner-editor.component.scss']
})
export class BookContentInnerEditorComponent implements OnInit {

  @Input() isActive = false;

  bookChapter: BookChapter = null;
  tinyMCEApiKey = '';//environment.tinyMCEApiKey;

  initConfig = {
    height: '100%',
    plugins: 'link lists image imagetools paste media advlist',
    image_uploadtab: true,
    // toolbar: 'undo redo | bold italic | bullist numlist outdent indent',
    toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | table | fontsizeselect",

    automatic_uploads: true,
    images_upload_url: 'some',
    image_title: true,
    paste_data_images: true,
    file_picker_types: 'image',
    
    images_upload_handler: (blobInfo, success, failure)=>{
      success(`data:${blobInfo.blob().type};base64,${blobInfo.base64()}`);
    }
  }

  constructor(
    private chapterEditingSvc: BookChapterEditingService   
  ) { }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe((chapter: BookChapter)=>{
      this.bookChapter = chapter;
    })
  }

  change($event){
    this.bookChapter.content = $event.editor.contentDocument.body.innerHTML;
  }
}
