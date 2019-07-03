import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection, HubConnectionState } from '@aspnet/signalr';
import { SecurityService } from '@br/core/services/security.service';
import { LogoutData } from '@br/core/models';
import { environment } from '@br/env/environment';
import { from, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserHubService {
  connection: HubConnection;

  constructor(
    private security: SecurityService
  ) {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.baseApiUrl + 'hub/user', { accessTokenFactory: () => this.security.token })
      .build();


    this.connection.on('GetStats', (val: any) => {
      console.log(val);
      alert(val);
    });

    this.connection.on('Logout', (data: LogoutData) => {
      debugger
      this.logOut(data);
    });
  }

  logOut(data: LogoutData) {
    console.warn('LOGOUT User!!!');
    this.security.logout(data);
    this.connection.stop();
  }

  init() {
    if (this.connection.state !== HubConnectionState.Connected) {
      
      const promise =  this.connection.start();
      promise.then(val => {
        console.log(val);
      });

      return from(promise);
    }

    return of(null);
  }

  stop() {
    this.connection.stop();
  }

  checkStats(val: any) {
    this.connection.invoke('CheckStatistics', val)
      .then((ok) => {
        console.log(ok);
      });
  }
}
