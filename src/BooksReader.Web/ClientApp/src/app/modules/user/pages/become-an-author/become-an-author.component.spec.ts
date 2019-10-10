import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BecomeAnAuthorComponent } from './become-an-author.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { MaterialModule } from '@br/material/material.module';

describe('BecomeAnAuthorComponent', () => {
  let component: BecomeAnAuthorComponent;
  let fixture: ComponentFixture<BecomeAnAuthorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BecomeAnAuthorComponent ],
      imports:[
        MaterialModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BecomeAnAuthorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
