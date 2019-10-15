import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllDomainsComponent } from './all-domains.component';
import { MaterialModule } from '@br/material/material.module';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AllDomainsComponent', () => {
  let component: AllDomainsComponent;
  let fixture: ComponentFixture<AllDomainsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllDomainsComponent ],
      imports:[
        MaterialModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllDomainsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


});
