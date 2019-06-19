import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookEditDialogComponent } from './book-edit-dialog.component';
import { MOCKED_PROVIDERS } from '../../../../tests/mocks/mockedProviders';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/modules/material/material.module';

describe('BookEditDialogComponent', () => {
  let component: BookEditDialogComponent;
  let fixture: ComponentFixture<BookEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookEditDialogComponent ],
      imports: [ BrowserAnimationsModule, FormsModule, MaterialModule ],
      providers: MOCKED_PROVIDERS
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
