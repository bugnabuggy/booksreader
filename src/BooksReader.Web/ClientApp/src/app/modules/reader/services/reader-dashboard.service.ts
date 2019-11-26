import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReaderDashboardFilters, WebResult, ReaderDashboardDto, OperationResult } from '@br/core/models';
import { Endpoints } from '@br/config';

@Injectable({
  providedIn: 'root'
})
export class ReaderDashboardService {

  constructor(
    private http: HttpClient
  ) { }

  getReaderBooks(filters: ReaderDashboardFilters) {
    const url = Endpoints.api.reader.books.replace('/{id}','');

    let keys = Object.keys(filters);
    var filterHeaders = {};
    keys.forEach(val => filterHeaders[val] = filters[val])
    
    let observable = this.http.get<WebResult<ReaderDashboardDto[]>>(url, {
            params: filterHeaders
        })
        
    return observable;
  }

  removeSubscription(sub: ReaderDashboardDto) {
    const url = Endpoints.api.reader.books.replace('{id}', sub.book.bookId);

    const observable = this.http.delete<OperationResult<any>>(url);

    return observable;
  }
}
