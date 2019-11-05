import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { AdminAllBooksFilter, AdminBookVerificationRequest } from '../models/api/requests/admin';
import { share } from 'rxjs/operators';
import { WebResult, AdminBookDto, OperationResult } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AdminBookService {

  constructor(
    private http: HttpClient
  ) { }

  list(filters: AdminAllBooksFilter) {
    const url = Endpoints.api.admin.books.all;

    let keys = Object.keys(filters);
    var filterHeaders = {};
    keys.forEach(val => filterHeaders[val] = filters[val])

    const observable = this.http.get<WebResult<AdminBookDto[]>>(url, {
      params: filterHeaders
    }).pipe(share());

    return observable;
  }

  toggleVerification(item: AdminBookDto) {
    const url = Endpoints.api.admin.books.verification;
    const data = {
      bookId: item.bookId,
      verified: !item.verified
    } as AdminBookVerificationRequest;

    const observable = this.http.post<OperationResult<AdminBookDto>>(url, data);

    return observable;
  }
}
