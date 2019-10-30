import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStringDialogComponent } from './add-string-dialog.component';
import { COMMON_VISUAL_MODULES } from '@br/test/common-dependencies-modules';
import { TranslateModule } from '@ngx-translate/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('AddStringDialogComponent', () => {
  let component: AddStringDialogComponent;
  let fixture: ComponentFixture<AddStringDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddStringDialogComponent ],
      imports: [
        NoopAnimationsModule,
        COMMON_VISUAL_MODULES,
        TranslateModule.forRoot()
      ],
      providers:[
        { provide: MatDialogRef, useValue: {}},
        { provide: MAT_DIALOG_DATA, useValue: {}},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddStringDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
