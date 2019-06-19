import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';
import { BookEditRequest } from '@br/core/models';

@Injectable()
export class AuthorBooksService {

    constructor(
        public http: HttpClient
    ) {}

    getBooks() {
        const url = Endpoints.api.author.book.replace('{id}', '');
        const observable = this.http.get(url)
        .pipe(share());

        return observable;
    }

    addBook(book: BookEditRequest ) {
        const url = Endpoints.api.author.book.replace('{id}', '');
        const observable = this.http.post(url, book).pipe(
            share());

        return observable;
    }

    updateBook(book: BookEditRequest ) {
        const url = Endpoints.api.author.book.replace('{id}', book.id);
        const observable = this.http.put(url, book).pipe(
            share());

        return observable;
    }

    deleteBook(id: string) {
        const url = Endpoints.api.author.book.replace('{id}', id);
        const observable = this.http.delete(url).pipe(
            share());

        return observable;
    }
}
