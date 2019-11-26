import { StandardFilters } from './standard.filters';

export interface BooksFilters extends StandardFilters {
    userId?: string;
    title?: string;
    description?: string;
    author?: string;
    isForSale?: boolean;
}
