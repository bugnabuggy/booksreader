import { OrderingFilter } from './ordering.filters';
import { PaginationFilter } from './pagination.filters';

export interface StandardFilters extends OrderingFilter, PaginationFilter {
    
}
