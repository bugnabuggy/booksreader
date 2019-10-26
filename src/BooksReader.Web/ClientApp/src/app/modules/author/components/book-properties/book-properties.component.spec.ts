import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookPropertiesComponent } from './book-properties.component';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { Book } from '@br/core/models';
import { bookMock } from '@br/test/mock-data';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookPropertiesComponent', () => {
  let component: BookPropertiesComponent;
  let fixture: ComponentFixture<BookPropertiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookPropertiesComponent ],
      imports:[
        SharedModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookPropertiesComponent);
    component = fixture.componentInstance;
    component.book = bookMock;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
