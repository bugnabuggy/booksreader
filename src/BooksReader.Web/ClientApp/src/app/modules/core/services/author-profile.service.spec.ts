import { TestBed } from '@angular/core/testing';

import { AuthorProfileService } from './author-profile.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockStorageService } from '@br/tests/mocks';
import { TestModule } from '@br/tests/test.module';
import { TranslateModule } from '@ngx-translate/core';

describe('AuthorProfileService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[
      TestModule,
      TranslateModule.forRoot()
    ],
    providers:[
    ]
  }));

  it('should be created', () => {
    const service: AuthorProfileService = TestBed.get(AuthorProfileService);
    expect(service).toBeTruthy();
  });
});
