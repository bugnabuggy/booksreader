import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderComponent } from './header.component';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from '@br/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MockStorageService } from '@br/tests/mocks';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        NoopAnimationsModule,
        SharedModule,
        RouterTestingModule,
        HttpClientTestingModule,
        MaterialModule,
        TranslateModule.forRoot()
      ],
      providers:[
        { provide: Storage, useValue: new MockStorageService() }
      ],
      declarations: [ HeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
