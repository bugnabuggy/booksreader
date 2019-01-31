import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookMarketComponent } from './book-market.component';
import { MOCKED_PROVIDERS } from '../../../../tests/mocks/mockedProviders';

describe('BookMarketComponent', () => {
  let component: BookMarketComponent;
  let fixture: ComponentFixture<BookMarketComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookMarketComponent ],
      providers: MOCKED_PROVIDERS
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookMarketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
