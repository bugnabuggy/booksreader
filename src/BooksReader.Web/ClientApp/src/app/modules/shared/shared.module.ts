import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';

import { DomainsListComponent } from './components/domains-list/domains-list.component';
import { DomainsListItemComponent } from './components/domains-list-item/domains-list-item.component';
import { TranslateModule } from '@ngx-translate/core';
import { PublicPageEditorComponent } from './components/public-page-editor/public-page-editor.component';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { ImageLoaderComponent } from './components/image-loader/image-loader.component';

@NgModule({
  declarations: [
    DomainsListComponent,
    DomainsListItemComponent,
    PublicPageEditorComponent,
    ConfirmationDialogComponent,
    ImageLoaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule.forChild()
  ],
  exports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    
    DomainsListComponent,
    DomainsListItemComponent,
    PublicPageEditorComponent,
    ConfirmationDialogComponent,
    ImageLoaderComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ]
})
export class SharedModule { }
