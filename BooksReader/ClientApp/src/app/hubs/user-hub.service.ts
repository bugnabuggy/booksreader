import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import { SecurityService } from '../services';


@Injectable({
  providedIn: 'root'
})
export class UserHubService {
  connection: HubConnection;

  constructor(
    private security: SecurityService
  ) {
    this.connection = new HubConnectionBuilder()
      .withUrl('/hub/user', { accessTokenFactory: () => this.security.token })
      .build();


    this.connection.on('GetStats', (val: any) => {
      console.log(val);
      alert(val);
    });

    this.connection.on('Logout', (val: any) => {
      console.warn('LOGOUT!!!');
      security.logout();
      this.connection.stop();
      alert('logout');
    });
  }

  init() {
    this.connection.start().then(val => {
      console.log(val);
    });
  }

  checkStats(val: any) {
    this.connection.invoke('CheckStatistics', val)
      .then((ok) => {
        console.log(ok);
      });
  }
}
