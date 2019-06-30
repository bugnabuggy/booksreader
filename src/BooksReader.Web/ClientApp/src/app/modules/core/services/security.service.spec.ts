import { TestBed } from '@angular/core/testing';
import { SecurityService } from './security.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { StorageService } from './storage.service';
import { MockStorageService } from '@br/tests/mocks/services';
import { SiteConstants, Endpoints } from '@br/config';
import { AuthResponse } from '../models';

describe('SecurityService', () => {

  const mockTokens = {
    access_token: 'test',
    expires_in: 120,
    token_type: 'bearer'
  } as AuthResponse;

  const storage = new MockStorageService({
    [SiteConstants.storageKeys.userToken]: JSON.stringify(mockTokens)
  });

  let httpTestingController: HttpTestingController;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      RouterTestingModule
    ],
    providers: [
      {
        provide: StorageService,
        useValue: new StorageService(storage)
      }
    ]
  }));

  it('should be created and get and save tokens form storage', () => {
    const service: SecurityService = TestBed.get(SecurityService);
    expect(service).toBeTruthy();
    expect(service.token).toEqual('test');
  });

  it('should ask for user info on initialization', () => {
    const service: SecurityService = TestBed.get(SecurityService);
    service.init();

    httpTestingController = TestBed.get(HttpTestingController);
    const req = httpTestingController.expectOne(Endpoints.api.user.info);

    // Assert that the request is a GET.
    expect(req.request.method).toEqual('GET');

    // Respond with mock data, causing Observable to resolve.
    // Subscribe callback asserts that correct data was returned.
    req.flush({});

    // Finally, assert that there are no outstanding requests.
    httpTestingController.verify();

  })
});
