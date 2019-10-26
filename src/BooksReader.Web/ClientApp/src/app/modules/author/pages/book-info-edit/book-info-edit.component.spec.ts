import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookInfoEditComponent } from './book-info-edit.component';
import { BookPropertiesComponent } from '../../components';
import { BookPricesEditorComponent, PublicPageEditorComponent } from '@br/shared/components';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { bookMock } from '@br/test/mock-data';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('BookInfoEditComponent', () => {
  let component: BookInfoEditComponent;
  let fixture: ComponentFixture<BookInfoEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookInfoEditComponent,
        BookPropertiesComponent,
       ],
      imports:[
        SharedModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookInfoEditComponent);
    component = fixture.componentInstance;
    component.book = bookMock
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
