import { Book } from '../../api-contracts';
import { BookSelectionType } from '@br/core/enums';

export interface BookSelection {
    book: Book;
    event: BookSelectionType;
}