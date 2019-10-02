import { Injectable } from '@angular/core';
import { ICrudService } from './interfaces';
import { HttpClient } from '@angular/common/http';
import { OperationResult, StandardFilters } from '../models';
import { Observable } from 'rxjs';
import { share } from 'rxjs/operators';
import { IIdentifiable } from '../models/site/generics';

export class CrudService<T extends IIdentifiable> implements ICrudService<T> {
  constructor(
    protected http: HttpClient,
    protected baseUrl: string
  ) { 
  }

  add(item: T): Observable<OperationResult<T>> {
    let observable = this.http.post<OperationResult<T>>(`${this.baseUrl}`, item)
    return observable.pipe(share());
  }

  // get particular item by id
  get(id: string): Observable<OperationResult<T>> {
    let observable = this.http.get<OperationResult<T>>(`${this.baseUrl}/${id}`)
    return observable.pipe(share());
  }

  // get generic list by filters
  list(filters: StandardFilters): Observable<OperationResult<T[]>> {
    let keys = Object.keys(filters);
    var filterHeaders = {};
    keys.forEach(val => filterHeaders[val] = filters[val] )

    let observable = this.http.get<OperationResult<T[]>>(`${this.baseUrl}`, {
      params: filterHeaders
    })
    return observable.pipe(share());
  }

  update(item: T): Observable<OperationResult<T>> {
    let observable = this.http.put<OperationResult<T>>(`${this.baseUrl}/${item.id}`, item);
    return observable.pipe(share());
  }

  delete(id: string): Observable<OperationResult<T>> {
    let observable = this.http.delete<OperationResult<T>>(`${this.baseUrl}/${id}`);
    return observable.pipe(share());
  }
}
