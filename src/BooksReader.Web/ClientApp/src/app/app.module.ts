import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {  MODULE_COMPONENTS,
  MODULE_SERVICES,
  MODULE_PIPES,
  MODULE_ENTRY_COMPONENTS,
  MATERIAL_DESIGN } from './moduleExports';

import { SimpleHttpInterceptor } from './interceptors/simple-http.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    MODULE_COMPONENTS,
    MODULE_PIPES,
  ],
  entryComponents: [
    MODULE_ENTRY_COMPONENTS
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MATERIAL_DESIGN,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [
    MODULE_SERVICES,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SimpleHttpInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
