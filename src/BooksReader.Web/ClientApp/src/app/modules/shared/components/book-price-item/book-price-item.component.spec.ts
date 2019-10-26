import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookPriceItemComponent } from './book-price-item.component';
import { MaterialModule } from '@br/material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { priceMock } from '@br/test/mock-data';

describe('BookPriceItemComponent', () => {
  let component: BookPriceItemComponent;
  let fixture: ComponentFixture<BookPriceItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookPriceItemComponent ],
      imports: [
        MaterialModule,
        ReactiveFormsModule,
        HttpClientTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookPriceItemComponent);
    component = fixture.componentInstance;
    component.price = priceMock;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
