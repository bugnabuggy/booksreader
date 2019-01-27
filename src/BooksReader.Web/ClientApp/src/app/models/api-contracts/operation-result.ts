export interface OperationResult<T> {
    data: T[];
    success: boolean;
    messages: string[];
}
