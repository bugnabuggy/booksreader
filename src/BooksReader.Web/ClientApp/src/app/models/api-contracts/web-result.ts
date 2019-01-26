export interface WebResult<T> {
    data: T;
    total: number;
    success: boolean;
    messages: string[];
    pageSize: number;
    pageNumber: number;
}
