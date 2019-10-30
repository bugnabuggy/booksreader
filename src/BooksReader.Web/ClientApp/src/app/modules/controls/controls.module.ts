import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TranslateModule } from '@ngx-translate/core';

import { MaterialModule } from '@br/material/material.module';

import { 
    ImageLoaderComponent 
  } from './edits';

import { 
    ConfirmationDialogComponent,
    AddStringDialogComponent 
  } from './dialogs';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';






@NgModule({
  declarations: [
    ImageLoaderComponent,

    ConfirmationDialogComponent,
    AddStringDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    TranslateModule.forChild()
  ],
  exports: [
    ImageLoaderComponent,
    ConfirmationDialogComponent,
    AddStringDialogComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent,
    AddStringDialogComponent
  ]
})
export class ControlsModule { }
