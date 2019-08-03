import { StandardFilters } from './standard.filters';

export interface AuthorBookFilters extends StandardFilters {
    title?: string;
}
