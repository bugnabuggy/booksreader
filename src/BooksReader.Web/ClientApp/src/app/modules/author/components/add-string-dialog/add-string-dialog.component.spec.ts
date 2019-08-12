import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStringDialogComponent } from './add-string-dialog.component';

describe('AddStringDialogComponent', () => {
  let component: AddStringDialogComponent;
  let fixture: ComponentFixture<AddStringDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddStringDialogComponent ]
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
