import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderBooksListItemComponent } from './reader-books-list-item.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';
import { MaterialModule } from '@br/material/material.module';
import { readerDashboardDtoMock } from '@br/test/mock-data/reader-dashboard-dto.mock';


describe('ReaderBooksListItemComponent', () => {
  let component: ReaderBooksListItemComponent;
  let fixture: ComponentFixture<ReaderBooksListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReaderBooksListItemComponent ],
      imports: [
        COMMON_TESTING_MODULES,
        MaterialModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderBooksListItemComponent);
    component = fixture.componentInstance;
    component.bookSubscription = readerDashboardDtoMock;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
