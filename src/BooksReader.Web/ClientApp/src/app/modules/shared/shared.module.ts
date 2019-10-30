import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TranslateModule } from '@ngx-translate/core';

import { MaterialModule } from '@br/material/material.module';
import { ControlsModule } from '@br/controls/controls.module';

import { 
    DomainsListComponent,
    DomainsListItemComponent, 
    PublicPageEditorComponent, 
    BookPricesEditorComponent, 
    BookPriceItemComponent } from './components';


@NgModule({
  declarations: [
    DomainsListComponent,
    DomainsListItemComponent,
    PublicPageEditorComponent,
    BookPricesEditorComponent,
    BookPriceItemComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    ControlsModule,
    TranslateModule.forChild()
  ],
  exports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    ControlsModule,

    DomainsListComponent,
    DomainsListItemComponent,
    PublicPageEditorComponent,
    BookPricesEditorComponent,
    BookPriceItemComponent
  ],
  entryComponents: [
    
  ]
})
export class SharedModule { }
