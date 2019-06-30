import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SimpleHttpInterceptor } from '@br/core/interceptors';
import { BrIntegrationsModule } from '@br/integrations/br-integrations.module';
import { SharedModule } from '@br/shared/shared.module';
import { PublicModule } from '@br/public/public.module';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { createTranslateLoader } from './utilities/translate-loader-factory';


@NgModule({
  declarations: [
    AppComponent,
  ],
  entryComponents: [
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    SimpleNotificationsModule.forRoot(
      {
        position: ['top', 'right'],
      }
    ),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),
    SharedModule,
    BrIntegrationsModule,
    PublicModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SimpleHttpInterceptor,
      multi: true
    },
    {
      provide: Storage,
      useValue: localStorage
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
