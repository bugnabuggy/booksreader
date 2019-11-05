import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookContentReaderComponent } from './book-content-reader.component';
import { CoreModule } from '@br/core/core.module';
import { TranslateModule } from '@ngx-translate/core';

describe('BookContentReaderComponent', () => {
  let component: BookContentReaderComponent;
  let fixture: ComponentFixture<BookContentReaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookContentReaderComponent ],
      imports: [
        CoreModule,
        TranslateModule.forRoot()
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookContentReaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
