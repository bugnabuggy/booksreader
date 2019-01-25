import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection, HubConnectionState } from '@aspnet/signalr';
import { SecurityService } from '../services/security.service';
import { LogoutData } from '../models/api-contracts';


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

    this.connection.on('Logout', (data: LogoutData) => {
      this.logOut(data);
    });
  }

  logOut(data: LogoutData) {
    console.warn('LOGOUT User!!!');
    this.security.logoutData = data;
    this.security.logout(true);
    this.connection.stop();
  }

  init() {
    if (this.connection.state !== HubConnectionState.Connected) {
      this.connection.start().then(val => {
        console.log(val);
      });
    }

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
