import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

export function getErrorMessage(err: HttpErrorResponse | any) {

    let msg = err.data && err.data.messages || err.message;
    return msg;
}