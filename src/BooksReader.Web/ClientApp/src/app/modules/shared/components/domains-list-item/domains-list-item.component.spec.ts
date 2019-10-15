import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DomainsListItemComponent } from './domains-list-item.component';
import { SharedModule } from '@br/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';
import { UserDomain } from '@br/core/models';
import { TranslateModule } from '@ngx-translate/core';

describe('DomainsListItemComponent', () => {
  let component: DomainsListItemComponent;
  let fixture: ComponentFixture<DomainsListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DomainsListItemComponent ],
      imports: [
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        TranslateModule.forRoot()
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DomainsListItemComponent);
    component = fixture.componentInstance;
    
    component.isEdit = false;
    component.domain = {} as UserDomain;
    
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
