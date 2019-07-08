import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangePasswordDialogComponent } from './change-password-dialog.component';
import { MaterialModule } from '@br/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';

describe('ChangePasswordDialogComponent', () => {
  let component: ChangePasswordDialogComponent;
  let fixture: ComponentFixture<ChangePasswordDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        MaterialModule,
        SharedModule,
        TranslateModule.forRoot(),
        RouterTestingModule,
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
