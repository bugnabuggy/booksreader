import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { share } from 'rxjs/operators';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private notifications: NotificationService,
  ) { }

  init() {
    let observable = of(null).pipe(
      share()
    );

    observable.subscribe(val => {

    }, 
    err => {

    })

    return observable;
  }
  
}
