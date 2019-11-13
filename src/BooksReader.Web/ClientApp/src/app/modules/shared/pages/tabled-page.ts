import { StandardFilters } from '@br/core/models';
import { SiteConstants } from '@br/config';

export class TabledPage<T, F extends StandardFilters>  {

    data: T[] = [];
    filters: F = {
        pageNumber: 0,
        pageSize: SiteConstants.defaultPageSize
    } as F;

    totalRecords = 0;
    uiIsBlocked = false;

    itemsPerPageArr = SiteConstants.itemsPerPage;

    constructor() {}

    getData(filters) {
        // empty method which will be overrided in descendants
    }

    pageChanged(event ) {
        
        this.filters.pageNumber = event.pageIndex;
        this.filters.pageSize = event.pageSize;
        this.getData(this.filters);
    }
}
