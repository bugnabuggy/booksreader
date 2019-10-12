import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@br/material/material.module';

import { DomainsListComponent } from './components/domains-list/domains-list.component';
import { DomainsListItemComponent } from './components/domains-list-item/domains-list-item.component';

@NgModule({
  declarations: [
    DomainsListComponent,
    DomainsListItemComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
  ],
  exports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    
    DomainsListComponent,
    DomainsListItemComponent
  ]
})
export class SharedModule { }
