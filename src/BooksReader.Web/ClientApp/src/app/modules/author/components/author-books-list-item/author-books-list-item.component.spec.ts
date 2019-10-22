import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorBooksListItemComponent } from './author-books-list-item.component';
import { MaterialModule } from '@br/material/material.module';
import { Book } from '@br/core/models';

describe('AuthorBooksListItemComponent', () => {
  let component: AuthorBooksListItemComponent;
  let fixture: ComponentFixture<AuthorBooksListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthorBooksListItemComponent ],
      imports:[ 
        MaterialModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorBooksListItemComponent);
    component = fixture.componentInstance;
    component.book = {} as Book;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
