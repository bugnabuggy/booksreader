import { BooksFilters } from '@br/core/models';

export interface ReaderDashboardFilters extends BooksFilters {
        subscriptionFrom?: Date;
        subscriptionTo?: Date;
}
