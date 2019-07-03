import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './pages/profile/profile.component';
import { MaterialModule } from '@br/material/material.module';
import { SharedModule } from '@br/shared/shared.module';
import { ChangePasswordDialogComponent } from './components/change-password-dialog/change-password-dialog.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [ProfileComponent, ChangePasswordDialogComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    MaterialModule,
    SharedModule,
    TranslateModule.forChild()
  ],
  entryComponents: [
    ChangePasswordDialogComponent
  ]
})
export class UserModule { }
