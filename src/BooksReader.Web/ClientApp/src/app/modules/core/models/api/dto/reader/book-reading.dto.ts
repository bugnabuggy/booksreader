import { GeneralBook } from '../general-book';
import { ChapterReadingDto } from './chapter-reading,dto';

export interface BookReadingDto {
    book: GeneralBook;
    chapters: ChapterReadingDto[];
    sessionId: string;
}
