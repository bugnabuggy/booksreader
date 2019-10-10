import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './pages/profile/profile.component';
import { SharedModule } from '@br/shared/shared.module';
import { ChangePasswordDialogComponent } from './components/change-password-dialog/change-password-dialog.component';
import { TranslateModule } from '@ngx-translate/core';
import { BecomeAnAuthorComponent } from './pages/become-an-author/become-an-author.component';


@NgModule({
  declarations: [
    ProfileComponent, 
    ChangePasswordDialogComponent, BecomeAnAuthorComponent],
  imports: [
    SharedModule,
    TranslateModule.forChild(),
    UserRoutingModule
  ],
  entryComponents: [
    ChangePasswordDialogComponent
  ]
})
export class UserModule { }
