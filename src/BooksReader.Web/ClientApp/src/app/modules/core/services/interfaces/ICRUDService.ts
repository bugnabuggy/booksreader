import { IIdentifiable, OperationResult, StandardFilters } from '@br/core/models';
import { Observable } from 'rxjs';

export interface ICrudService<T extends IIdentifiable>  {
    add(item: T): Observable<OperationResult<T>>;
    get(id: string): Observable<OperationResult<T>>;
    list(filters: StandardFilters): Observable<OperationResult<T[]>>;
    update(item: T): Observable<OperationResult<T>>;
    delete(id: string): Observable<OperationResult<T>>;
}
