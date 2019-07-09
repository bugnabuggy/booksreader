import { TestBed, fakeAsync, flushMicrotasks } from '@angular/core/testing';

import { MockStorageService } from '@br/tests/mocks/services';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateModule } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { UserService } from './user.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserRegistration } from '../models/api-contracts/user-registration.contract';
import { Endpoints } from '@br/config';
import { AuthResponse, AppUser } from '../models';
import { timer } from 'rxjs';
import { Router } from '@angular/router';
import { authMockResponse, appUser } from '@br/tests/mocks/responses';

let router = {
  navigate: jasmine.createSpy('navigate'),
  navigateByUrl: jasmine.createSpy('navigateByUrl')
}

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      NoopAnimationsModule,
      TranslateModule.forRoot(),
      RouterTestingModule,
      HttpClientTestingModule
    ],

    providers: [
      {
        provide: Storage,
        useValue: new MockStorageService()
      },
      { provide: Router, useValue: router }
    ]
  }));

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });

  it('should send five requests on registration and login and naviaget to profile', (done) => {
    const service: UserService = TestBed.get(UserService);

    const model = {
      fullname: 'Test Full',
      username: 'Test',

      password: '123'
    } as UserRegistration;

    service.registration(model).subscribe(val => {
      expect(router.navigateByUrl).toHaveBeenCalledWith(Endpoints.forntend.user.profileUrl);
      done();
    }, err => {
      expect(true).toBeFalsy('Error while registering ana account');
      done();
    })
    let httpTestingController = TestBed.get(HttpTestingController);
    
    const reqAf = httpTestingController.expectOne(Endpoints.api.authorization.antiforgery);
    expect(reqAf.request.method).toEqual('GET');
    reqAf.flush('key');

    const reqRg = httpTestingController.expectOne(Endpoints.api.authorization.registration);
    expect(reqRg.request.method).toEqual('POST');
    reqRg.flush(null);
    
    const reqUt = httpTestingController.expectOne(Endpoints.api.authorization.login);
    expect(reqRg.request.method).toEqual('POST');
    reqUt.flush(authMockResponse);
    
    const reqUi = httpTestingController.expectOne(Endpoints.api.user.info);
    expect(reqAf.request.method).toEqual('GET');
    reqUi.flush(appUser);

    var timeout = timer(10).subscribe(()=>{
      timeout.unsubscribe();
      const reqLi = httpTestingController.expectOne(Endpoints.api.user.loginHistory);
      expect(reqLi.request.method).toEqual('POST');
      reqLi.flush(null);
      
      // Finally, assert that there are no outstanding requests.
      httpTestingController.verify();

    })
  })
});
