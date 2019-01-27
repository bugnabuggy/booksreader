import {OperationResult} from './operation-result';

export interface WebResult<T> extends OperationResult<T> {
    total: number;
    pageSize: number;
    pageNumber: number;
}
