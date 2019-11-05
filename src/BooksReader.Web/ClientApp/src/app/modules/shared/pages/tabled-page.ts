import { StandardFilters } from '@br/core/models';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';



export class TabledPage<T, F extends StandardFilters>  {

    data: T[];
    filters: F;
    totalRecords = 0;
    uiIsBlocked = false;

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
