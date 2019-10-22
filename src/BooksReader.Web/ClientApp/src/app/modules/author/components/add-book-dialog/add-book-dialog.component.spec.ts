import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBookDialogComponent } from './add-book-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ImageLoaderComponent } from '@br/shared/components';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AddBookDialogComponent', () => {
  let component: AddBookDialogComponent;
  let fixture: ComponentFixture<AddBookDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        AddBookDialogComponent,
        ImageLoaderComponent
       ],
      imports: [
        NoopAnimationsModule,
        MaterialModule,
        ReactiveFormsModule,
        COMMON_TESTING_MODULES
      ],
      providers: [
        { provide: MatDialogRef, useValue: {close: ()=>{} }},
        { provide: MAT_DIALOG_DATA, useValue: {}}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBookDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
