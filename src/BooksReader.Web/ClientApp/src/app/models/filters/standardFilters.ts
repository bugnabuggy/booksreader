import { OrderingFilter } from './orderingFilter';
import { PaginationFilter } from './paginationFilter';

export class StandardFilters implements OrderingFilter, PaginationFilter {
    pageNumber: number;
    pageSize: number;
    orderByField?: string;
    isDesc?: boolean;
}
