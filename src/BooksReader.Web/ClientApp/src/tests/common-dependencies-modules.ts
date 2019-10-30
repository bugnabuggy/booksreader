import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';

export const COMMON_VISUAL_MODULES = [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule
]


export const COMMON_TESTING_MODULES = [
    RouterTestingModule,
    NoopAnimationsModule,
    HttpClientTestingModule,
    TranslateModule.forRoot(),
    SimpleNotificationsModule.forRoot()
]