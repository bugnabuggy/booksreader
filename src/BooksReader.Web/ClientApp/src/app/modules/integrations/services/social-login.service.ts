import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { SocialUser, AuthService, FacebookLoginProvider } from 'angularx-social-login';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SocialLoginService {
  socialUser$ = new BehaviorSubject<SocialUser>(null);

  constructor(private authService: AuthService) {
  }

  init() {
    this.authService.authState.subscribe((user) => {
      this.socialUser$.next(user);
    });
  }

  signInWithFB(): Observable<SocialUser> {
    const promise = this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
    const observable = from(promise).pipe(share());

    observable.subscribe(user => {
      console.log(user);
    });

    return observable;
  }

  signOutWithFB(): Observable<any> {
    const promise = this.authService.signOut(true);
    const observable = from(promise).pipe(share());

    observable.subscribe(val => {
      console.log(val);
    });

    return observable;
  }
}
