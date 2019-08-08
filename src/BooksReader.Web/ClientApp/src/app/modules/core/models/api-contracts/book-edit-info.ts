import { Book } from './book.dto';
import { BookChapter } from './book-chapter.dto';
import { PersonalPage } from './entities';

export interface BookEditInfo {
    book: Book;
    chapters: BookChapter[];
    bookPage: PersonalPage;
}