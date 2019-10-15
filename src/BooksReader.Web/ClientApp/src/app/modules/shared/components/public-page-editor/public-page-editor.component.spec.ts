import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicPageEditorComponent } from './public-page-editor.component';
import { MaterialModule } from '@br/material/material.module';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { ReactiveFormsModule } from '@angular/forms';

describe('PublicPageEditorComponent', () => {
  let component: PublicPageEditorComponent;
  let fixture: ComponentFixture<PublicPageEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicPageEditorComponent ],
      imports:[
        MaterialModule,
        ReactiveFormsModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicPageEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
