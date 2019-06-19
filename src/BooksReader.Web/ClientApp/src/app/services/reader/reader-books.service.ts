import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';

@Injectable()
export class ReaderBooksService {

    constructor(
        public http: HttpClient
    ) { }
    getReaderBooks() {
        const url = Endpoints.api.reader.books;
        const observable = this.http.get(url)
            .pipe(share());

        return observable;
    }
}
