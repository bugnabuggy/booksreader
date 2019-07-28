import { NgModule } from '@angular/core';
import { RouterLinkStubDirective } from './mocks/router-link-stub.directive';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';
import { MockStorageService } from './mocks';
import { SimpleNotificationsModule, NotificationsService } from 'angular2-notifications';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
        RouterLinkStubDirective
    ],
    imports: [
    ],
    exports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NoopAnimationsModule
    ],
    providers: [
        {
            provide: Storage,
            useValue: new MockStorageService()
        },
        {
            provide: NotificationsService,
            useValue: {}
        }
    ],
    entryComponents: [],
    bootstrap: []
})
export class TestModule { }
