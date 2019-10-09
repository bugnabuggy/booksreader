import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { SimpleNotificationsModule } from 'angular2-notifications';


export const COMMON_TESTING_MODULES = [
    RouterTestingModule,
    HttpClientTestingModule,
    TranslateModule.forRoot(),
    SimpleNotificationsModule.forRoot()
]