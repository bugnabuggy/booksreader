import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';
import { TranslateModule } from '@ngx-translate/core';

import { 
    DomainsListComponent,
    DomainsListItemComponent, 
    PublicPageEditorComponent, 
    ConfirmationDialogComponent, 
    ImageLoaderComponent, 
    BookPricesEditorComponent, 
    BookPriceItemComponent } from './components';


@NgModule({
  declarations: [
    DomainsListComponent,
    DomainsListItemComponent,
    PublicPageEditorComponent,
    ConfirmationDialogComponent,
    ImageLoaderComponent,
    BookPricesEditorComponent,
    BookPriceItemComponent
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
    ImageLoaderComponent,
    BookPricesEditorComponent,
    BookPriceItemComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ]
})
export class SharedModule { }
