import { Injectable, Inject, Optional } from '@angular/core';
import { HubConnectionBuilder, HubConnection, HubConnectionState, HttpTransportType } from '@aspnet/signalr';
import { SecurityService } from '@br/core/services/security.service';
import { environment } from '@br/env/environment';
import { from, of } from 'rxjs';
import { LogoutData } from '@br/core/models';
import { SiteMessages } from '@br/config/site-messages';

@Injectable({
  providedIn: 'root'
})
export class UserHubService {
  private connection: HubConnection;
  private forceLogoutCallback: (data: LogoutData) => any

  constructor(
    private security: SecurityService,
    @Optional() @Inject('BASE_URL') private baseUrl: string 
  ) {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.baseApiUrl + 'hub/user', { accessTokenFactory: () => this.security.token, transport: HttpTransportType.WebSockets })
      .build();

    // events binding
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
    if(this.forceLogoutCallback) {
      this.forceLogoutCallback(data);
    }
  }

  init(logoutCallback) {
    if(!logoutCallback) {
      throw new Error(SiteMessages.system.noForceLogoutCallback);
    }

    this.forceLogoutCallback = logoutCallback;

    if (this.connection.state !== HubConnectionState.Connected) {
      const promise = this.connection.start();
      promise.then(val => {
        console.log(val);
      }, err=>{
        console.log(err);
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
