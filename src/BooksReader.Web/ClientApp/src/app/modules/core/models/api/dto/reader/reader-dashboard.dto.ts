import { GeneralBook } from '../general-book';
import { BookSubscriptionDto } from '../book-subscription.dto';

export interface ReaderDashboardDto {
    book: GeneralBook;
    subscription: BookSubscriptionDto;
}
