import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DomainsListComponent } from './domains-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';
import { DomainsListItemComponent } from '..';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('DomainsListComponent', () => {
  let component: DomainsListComponent;
  let fixture: ComponentFixture<DomainsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        DomainsListComponent,
        DomainsListItemComponent,
       ],
      imports:[
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DomainsListComponent);
    component = fixture.componentInstance;

    component.domains = [];

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
