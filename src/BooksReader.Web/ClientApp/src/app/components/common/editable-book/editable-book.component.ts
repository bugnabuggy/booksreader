import { Component, OnInit } from '@angular/core';
import { BookComponent } from '../book/book.component';


@Component({
  selector: 'app-editable-book',
  templateUrl: './editable-book.component.html',
  styleUrls: ['./editable-book.component.scss']
})
export class EditableBookComponent extends BookComponent {

}
