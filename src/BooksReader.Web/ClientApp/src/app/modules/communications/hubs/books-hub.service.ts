import { Injectable, Optional, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HttpTransportType, HubConnectionState } from '@aspnet/signalr';
import { SecurityService } from '@br/core/services/security.service';
import { environment } from '@br/env/environment';
import { from, of, Observable } from 'rxjs';
import { flatMap, share } from 'rxjs/operators';
import { OperationResult, BookReadingDto, BookChapter } from '@br/core/models';

@Injectable({
  providedIn: 'root'
})
export class BooksHubService {
  private connection: HubConnection;

  
  constructor(
    private security: SecurityService,
    @Optional() @Inject('BASE_URL') private baseUrl: string 
  ) { 
    
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.baseApiUrl + 'hub/books', { accessTokenFactory: () => this.security.token, transport: HttpTransportType.WebSockets })
      .build();


    // on disconnect can't continue getting new data or reading 
    this.connection.onclose(err=>{
      console.log(err);
    })

    // events binding
    this.connection.on('BookNotification', (val) => {
      
    });
  }

  init() {
    if (this.connection.state !== HubConnectionState.Connected) {
      const promise = this.connection.start();
      promise.then(val => {
        console.log('book hub connected');
      }, err=>{
        console.log(err);
      });

      return from(promise);
    }
    return of(null);
  }

  getBook(bookId: string): Observable<OperationResult<BookReadingDto>> {
    let init = this.init();
    const observable = init.pipe(
      flatMap(val => this.connection.invoke('GetBook', bookId))
    ).pipe(share());

    return observable;
  }

  getChapter(chapterId: string, bookId: string): Observable<OperationResult<BookChapter>> {
    const observable = from(this.connection.invoke('GetChapter', bookId, chapterId))
      .pipe(share());

    return observable;
  }
}
