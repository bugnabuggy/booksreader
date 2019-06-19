import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {  MODULE_COMPONENTS,
  MODULE_SERVICES,
  MODULE_ENTRY_COMPONENTS,
   } from './moduleExports';

import { SimpleHttpInterceptor } from '@br/core/interceptors';
import { BrIntegrationsModule } from '@br/integrations/br-integrations.module';
import { SharedModule } from '@br/shared/shared.module';
import { PublicModule } from '@br/public/public.module';
import { SimpleNotificationsModule } from 'angular2-notifications';


@NgModule({
  declarations: [
    AppComponent,
    MODULE_COMPONENTS,
  ],
  entryComponents: [
    MODULE_ENTRY_COMPONENTS
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
