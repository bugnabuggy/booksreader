import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReaderDashboardComponent } from './reader-dashboard.component';
import { ReaderBooksListComponent } from '../../components/reader-books-list/reader-books-list.component';
import { ReaderBooksListItemComponent } from '../../components/reader-books-list-item/reader-books-list-item.component';

describe('ReaderDashboardComponent', () => {
  let component: ReaderDashboardComponent;
  let fixture: ComponentFixture<ReaderDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        ReaderDashboardComponent,
        ReaderBooksListComponent,
        ReaderBooksListItemComponent
       ],
      imports: [
        
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReaderDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
