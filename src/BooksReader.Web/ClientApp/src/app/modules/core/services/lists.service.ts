import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { TypesList } from '../models/api';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ListsService {
  
  readonly _lists = new BehaviorSubject<TypesList[]>([]);

  get lists ()  { return this._lists; }

  constructor(
    private http: HttpClient
  ) { }

  getList() : Observable<TypesList[]>{
    const url = Endpoints.api.public.lists;
    const observabel =  this.http.get<TypesList[]>(url).pipe(share());
    
    return observabel;
  }

  init():  Observable<TypesList[]> {
    var observable = this.getList();

    observable.subscribe(x => {
      if(x) {
        this._lists.next(x);
      }
    })

    return observable;
  }
}
