import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadBookComponent } from './read-book.component';
import { ReadingModule } from '@br/reading/reading.module';
import { RouterTestingModule } from '@angular/router/testing';
import { COMMON_TESTING_MODULES } from '@br/test/common-dependencies-modules';

describe('ReadBookComponent', () => {
  let component: ReadBookComponent;
  let fixture: ComponentFixture<ReadBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        ReadBookComponent,
       ],
      imports: [
        ReadingModule,
        COMMON_TESTING_MODULES
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
