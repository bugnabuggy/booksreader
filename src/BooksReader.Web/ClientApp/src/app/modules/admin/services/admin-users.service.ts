import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { share } from 'rxjs/operators';
import { Endpoints } from '@br/config';

@Injectable({
  providedIn: 'root'
})
export class AdminUsersService {  
  constructor(
    private http: HttpClient
  ) { }


  getUsers() {
    const url = Endpoints.api.users.all;
    const observable = this.http.get(url).pipe(share());

    observable.subscribe((val) => {
      console.log(val);
    });

    return observable;
  }

  addUserToRole(username: string, role: string) {
    let url = Endpoints.api.users.userRole;
    url = url.replace('{username}', username);
    url = url.replace('{role}', role);
    return this.http.post(url, {}).pipe(share());
  }

  removeUserFromRole(username: string, role: string) {
    let url = Endpoints.api.users.userRole;
    url = url.replace('{username}', username);
    url = url.replace('{role}', role);

    return this.http.delete(url).pipe(share());
  }

  delete(username: string) {
    let url = Endpoints.api.users.delete
      .replace("{username}", username);

    const observable = this.http.delete(url).pipe(share());
    return observable;
  }
}
