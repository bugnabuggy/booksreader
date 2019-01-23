import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
export class UserHubService {
  connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('/hub/user')
      .build();


    this.connection.on('GetStats', (val: any) => {
      debugger;
      console.log(val);
    });

    this.connection.start().then(val => {
      debugger;
      console.log(val);
    });
  }

  checkStats(val: any) {
    debugger;
    this.connection.invoke('CheckStatistics', val)
      .then((ok) => {
        debugger;
        console.log(ok);
      });
  }
}
