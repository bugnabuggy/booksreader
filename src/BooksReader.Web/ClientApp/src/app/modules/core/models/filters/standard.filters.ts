import { OrderingFilter } from './ordering.filters';
import { PaginationFilter } from './pagination.filters';

export class StandardFilters implements OrderingFilter, PaginationFilter {
    pageNumber?: number;
    pageSize?: number;
    orderByField?: string;
    isDesc?: boolean;
}
