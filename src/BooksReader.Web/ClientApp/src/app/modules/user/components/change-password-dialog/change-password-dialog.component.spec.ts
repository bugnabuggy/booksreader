import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChangePasswordDialogComponent } from './change-password-dialog.component';
import { MaterialModule } from '@br/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SecurityService, NotificationService } from '@br/core/services';
import { of } from 'rxjs';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';



const securitySvcSpy = {
  changePassword: jasmine.createSpy('changePassword', (form) => { return of(null)}),
};

const notificationsSvcSpy = {
  showSuccess:jasmine.createSpy('showSuccess'),
  showError:jasmine.createSpy('showError'),
}

describe('ChangePasswordDialogComponent', () => {
  let component: ChangePasswordDialogComponent;
  let fixture: ComponentFixture<ChangePasswordDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        NoopAnimationsModule,
        MaterialModule,
        SharedModule,
        TranslateModule.forRoot(),
      ],
      providers:[
        {provide: SecurityService, useFactory: ()=>{ return securitySvcSpy} },
        {provide: NotificationService, useFactory: ()=>{ return notificationsSvcSpy} },
        { provide: MAT_DIALOG_DATA, useValue: {} },
        { provide: MatDialogRef, useValue: {} }
      ],
      declarations: [ ChangePasswordDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangePasswordDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
