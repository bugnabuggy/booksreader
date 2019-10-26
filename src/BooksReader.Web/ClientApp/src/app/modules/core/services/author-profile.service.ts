import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Endpoints } from '@br/config';
import { share, finalize } from 'rxjs/operators';
import { UserDomain, AuthorFullProfile, AuthorProfile } from '../models';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorProfileService {

  private _domais$ = new BehaviorSubject<UserDomain[]>([]);
  private _profile: AuthorProfile;

  private authorfullProfle$: Observable<AuthorFullProfile>;

  constructor(
    private http: HttpClient
  ) { }

  get domains$() {
    return this._domais$;
  }

  get author() {
    return this._profile;
  }

  getFullProfile():Observable<AuthorFullProfile> {
    if (this.authorfullProfle$) {
      return this.authorfullProfle$;
    }

    const url = Endpoints.api.author.fullProfile;

    this.authorfullProfle$ = this.http.get<AuthorFullProfile>(url)
      .pipe(
        share(),
        finalize(()=>{
          this.authorfullProfle$ = null;
        }));

    this.authorfullProfle$.subscribe((val) => {
      this._domais$.next(val.domains);
      
      this._profile = {
        authorName: val.authorName,
        description: val.description,
        userId: val.userId,
        id: val.id
      } as AuthorProfile;
    })

    return this.authorfullProfle$;
  }

}
