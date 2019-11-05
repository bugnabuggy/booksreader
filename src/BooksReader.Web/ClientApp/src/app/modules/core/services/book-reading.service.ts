import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BookChapter } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BookReadingService {

  activeChapter = new BehaviorSubject<BookChapter>(null);

  constructor() { }
}
