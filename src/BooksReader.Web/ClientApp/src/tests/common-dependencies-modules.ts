import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';


export const COMMON_TESTING_MODULES = [
    RouterTestingModule,
    NoopAnimationsModule,
    HttpClientTestingModule,
    TranslateModule.forRoot(),
    SimpleNotificationsModule.forRoot()
]