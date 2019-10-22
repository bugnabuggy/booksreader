import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorBooksListComponent } from './author-books-list.component';
import { MaterialModule } from '@br/material/material.module';
import { AuthorBooksListItemComponent } from '../author-books-list-item/author-books-list-item.component';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('AuthorBooksListComponent', () => {
  let component: AuthorBooksListComponent;
  let fixture: ComponentFixture<AuthorBooksListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        AuthorBooksListComponent,
        AuthorBooksListItemComponent
       ],
      imports: [
        MaterialModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorBooksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
