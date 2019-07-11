import { Injectable } from '@angular/core';
import { Endpoints } from '@br/config';
import { AuthorProfile } from '../models';
import { share } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorProfileService {

  constructor(
    private http: HttpClient,
    private userSvc: UserService
  ) { }

  getAuthorProfile() {
    const url = Endpoints.api.author.profile;
    const observable = this.http.get<AuthorProfile>(url).pipe(share());

    return observable;
  }

  updateAuthorProfile(profile: AuthorProfile) {
    const url = Endpoints.api.author.profile + `/${this.userSvc.user.username}`;
    const observable = this.http.put<AuthorProfile>(url, profile).pipe(share());

    return observable;
  }
}
