import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookPricesEditorComponent } from './book-prices-editor.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { MaterialModule } from '@br/material/material.module';
import { BookPriceItemComponent } from '../book-price-item/book-price-item.component';
import { ReactiveFormsModule } from '@angular/forms';

describe('BookPricesEditorComponent', () => {
  let component: BookPricesEditorComponent;
  let fixture: ComponentFixture<BookPricesEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        BookPricesEditorComponent,
        BookPriceItemComponent
       ],
      imports: [
        MaterialModule,
        ReactiveFormsModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookPricesEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
