import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '@br/core/models';
import { BookEditingService } from '@br/core/services';

@Component({
  selector: 'app-book-editing-page',
  templateUrl: './book-editing-page.component.html',
  styleUrls: ['./book-editing-page.component.scss']
})
export class BookEditingPageComponent implements OnInit {

  book: Book;

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private bookEditingSvc: BookEditingService
  ) { }

  ngOnInit() {
    
  }

}
