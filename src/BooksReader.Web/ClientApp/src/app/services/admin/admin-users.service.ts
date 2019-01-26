import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { share } from 'rxjs/operators';
import { Endpoints } from '../../enums/Endpoints';

@Injectable({
  providedIn: 'root'
})
export class AdminUsersService {

  constructor(
    private http: HttpClient
  ) { }


  getUsers() {
    debugger;
    const url = Endpoints.api.users.all;
    const observable = this.http.get(url).pipe(share());

    observable.subscribe((val) => {
      debugger;
      console.log(val);
    });

    return observable;
  }
}
