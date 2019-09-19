import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TypesList } from '../models';
import { Endpoints } from '@br/config';
import { Observable, BehaviorSubject } from 'rxjs';
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

  get(name: string){
    var list = this._lists.getValue().find(x=>x.name == name);

    // return empty object if ther is no list 
    return list || { values:[]};
  }

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
