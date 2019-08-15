import { Component, OnInit } from '@angular/core';
import { BookChapterEditingService } from '@br/core/services';
import { BookChapter } from '@br/core/models';
import { environment } from '@br/env/environment';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-book-content-inner-editor',
  templateUrl: './book-content-inner-editor.component.html',
  styleUrls: ['./book-content-inner-editor.component.scss']
})
export class BookContentInnerEditorComponent implements OnInit {

  bookChapter: BookChapter = null;
  tinyMCEApiKey = environment.tinyMCEApiKey;

  initConfig = {
    height: '100%',
    plugins: 'link image imagetools paste media',
    image_uploadtab: true,
    
    automatic_uploads: true,
    images_upload_url: 'some',
    image_title: true,
    paste_data_images: true,
    file_picker_types: 'image',
    // file_picker_callback: (cb,value,meta) => {
    //   cb('');
    // },
    images_upload_handler: (blobInfo, success, failure)=>{
      success(`data:${blobInfo.blob().type};base64,${blobInfo.base64()}`);
    }
  }

  constructor(
    private chapterEditingSvc: BookChapterEditingService   
  ) { 
  }

  ngOnInit() {
    this.chapterEditingSvc.activeChapter.subscribe((chapter: BookChapter)=>{
      this.bookChapter = chapter;
    })
  }

  change($event){
    this.bookChapter.content = $event.editor.contentDocument.body.innerHTML;
  }
}
