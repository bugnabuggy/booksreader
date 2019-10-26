import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookEditingComponent } from './book-editing.component';
import { SharedModule } from '@br/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { BookInfoEditComponent } from '../book-info-edit/book-info-edit.component';
import { BookPropertiesComponent } from '../../components';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('BookEditingComponent', () => {
  let component: BookEditingComponent;
  let fixture: ComponentFixture<BookEditingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookEditingComponent,
        BookInfoEditComponent,
        BookPropertiesComponent,
      ],
      imports:[
        SharedModule,
        RouterTestingModule,
        HttpClientTestingModule,
        NoopAnimationsModule,
        SimpleNotificationsModule.forRoot(),
        TranslateModule.forRoot()
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookEditingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
