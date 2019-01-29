import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookEditDialogComponent } from './book-edit-dialog.component';
import { MOCKED_PROVIDERS } from '../../../../tests/mocks/mockedProviders';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { MATERIAL_DESIGN } from '../../../moduleExports';

describe('BookEditDialogComponent', () => {
  let component: BookEditDialogComponent;
  let fixture: ComponentFixture<BookEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookEditDialogComponent ],
      imports: [ BrowserAnimationsModule, FormsModule, MATERIAL_DESIGN ],
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
