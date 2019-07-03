import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SecurityService, NotificationService } from '@br/core/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChangePasswordModel } from '@br/core/models';
import { passwordConfirmationValidator } from '@br/utilities/validators/password-confirmation.validator';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss']
})
export class ChangePasswordDialogComponent implements OnInit {

  changePassForm: FormGroup
  serverMessages:string[] = [];
  
  isUiBlocked = false;

  constructor(
    private securitySvc: SecurityService,
    private fb: FormBuilder,
    private notifications: NotificationService,
    public dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {

    this.changePassForm = this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmation: ['', Validators.required]
    }, { validators: passwordConfirmationValidator });
  }

  onClose(){
    this.dialogRef.close();
  }

  changePass(){
    this.isUiBlocked = true;
    this.securitySvc.changePassword(this.changePassForm.value as ChangePasswordModel)
    .pipe(finalize(()=>{
      this.isUiBlocked = false;
    }))
    .subscribe(val=>{
      this.notifications.showSuccess('PASSWORD_CHANGED');
      this.onClose();
    }, err => {
      if (err.status != 404){
        this.notifications.showError(err.error);
        this.serverMessages = err.error;
      } else {
        this.notifications.showError(err.message);
        this.serverMessages.push('REQUEST_ERROR');
      }

        
    });
  }
}
