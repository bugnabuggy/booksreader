import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

export function getErrorMessage(err: HttpErrorResponse | any) {
    debugger;
    let msg = '';

    // if error is operation result response
    if (err.error 
        && err.error.messages
        && err.error.messages.length > 0) {
            err.error.messages.forEach(element => {
            msg += element +'\n';
        });
        return msg;
    }

    // if error is http response
    if (typeof err.error == 'string') {
        return err.error
    }

    // if error is progress object 
    // if () {

    // }

    return err.message;
}