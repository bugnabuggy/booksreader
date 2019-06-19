import {OperationResult} from './operation-result';

export interface WebResult<T> extends OperationResult<T> {
    data: T;
    total: number;
    pageSize: number;
    pageNumber: number;
}
