import { Book } from './entities/book';
import { BookChapter } from './entities/book-chapter';
import { PersonalPage } from './entities';
import { BookPrice } from './entities/book-price';

export interface BookEditInfo {
    book: Book;
    bookPage: PersonalPage;
    chapters: BookChapter[];
    prices: BookPrice[];
}
