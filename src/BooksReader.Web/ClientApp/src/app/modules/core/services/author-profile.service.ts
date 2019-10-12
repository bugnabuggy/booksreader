import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorProfileService {


  constructor(
    private http: HttpClient
  ) { }

  getFullProfile() {
    const url = Endpoints.api.author.fullProfile;

    const observable = this.http.get(url).pipe(share());

    return observable;
  }

}
