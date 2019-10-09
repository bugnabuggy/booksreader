import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SimpleNotificationsModule } from 'angular2-notifications';
import { PublicModule } from './modules/public/public.module';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { createTranslateLoader } from '@br/utilities/translate-loader-factory';
import { BrHttpInterceptor } from '@br/core/interceptors';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'app'}),
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SimpleNotificationsModule.forRoot(
      {
        position: ['top', 'right'],
      }),
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useFactory: (createTranslateLoader),
          deps: [HttpClient]
        }
      }),
      PublicModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BrHttpInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
