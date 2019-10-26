import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Base64FileLoader } from '@br/utilities/base64-file-loader';
import { SiteConstants } from '@br/config';

@Component({
  selector: 'app-image-loader',
  templateUrl: './image-loader.component.html',
  styleUrls: ['./image-loader.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ImageLoaderComponent),
      multi: true
    }
  ]
})
export class ImageLoaderComponent implements ControlValueAccessor {

  
  @Input() defaultImage: string = '/assets/images/default-image.png';
  @Input() height = SiteConstants.defaultImageSelectorHeight;
  @Input() maxFileSize;

  @Output() pictureSelected = new EventEmitter<File>();
  @Output() deleteImage = new EventEmitter<string>();
  @Output() validationFailed = new EventEmitter<string>();

  image: string;
  input: HTMLInputElement;
  file: File;
  
  propagateChange = (_: any) => {};

  constructor() { }

  writeValue(obj: any): void {
      this.image = obj; 
  }
  registerOnChange(fn: any): void {
    console.log('registerOnChange');
    this.propagateChange = fn;
    // throw new Error("Method not implemented.");
  }
  registerOnTouched(fn: any): void {
    console.log('registerOnTouched');
    // throw new Error("Method not implemented.");
  }
  setDisabledState?(isDisabled: boolean): void {
    console.log('setDisabledState');
    // throw new Error("Method not implemented.");
  }

  fileSelected(input: HTMLInputElement) {
    this.input = input;
    const file = input.files[0];
    
    if (this.maxFileSize && file.size > this.maxFileSize) {
      this.validationFailed.emit('ALLOWED_FILE_SIZE_EXCEEDED');
      input.value = '';
      return;
    }

    this.file = file;
    Base64FileLoader.loadFile(file).subscribe(
      val => {
        this.image = val;
        this.propagateChange(val);
      }
    );
    this.pictureSelected.emit(file);
  }

  delete($event) {
    $event.preventDefault();

    if (this.file) {
      this.file = null;
      this.input.value = '';
    }
    
    this.image = '';
    this.propagateChange('');
    this.deleteImage.emit(this.image);
  }
}
