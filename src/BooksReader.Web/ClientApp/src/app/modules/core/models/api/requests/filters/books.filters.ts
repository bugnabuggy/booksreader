import { StandardFilters } from './standard.filters';

export interface BooksFilters extends StandardFilters {
    title: string;
    description: string;
}
