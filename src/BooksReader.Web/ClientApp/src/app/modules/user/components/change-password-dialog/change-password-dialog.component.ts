import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SecurityService } from '@br/core/services';
import { NotificationService } from '@br/core/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { passwordConfirmationValidator } from '@br/utilities/validators';
import { finalize } from 'rxjs/operators';
import { ChangePasswordRquest } from '@br/core/models';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrls: ['./change-password-dialog.component.scss']
})
export class ChangePasswordDialogComponent implements OnInit {
  changePassForm: FormGroup;
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
      newPassword: ['', [Validators.required, Validators.minLength(3)]],
      confirmation: ['', Validators.required]
    }, { validators: passwordConfirmationValidator });
  }

  onClose(){
    this.dialogRef.close();
  }

  changePass(){
    this.isUiBlocked = true;
    this.securitySvc.changePassword(this.changePassForm.value as ChangePasswordRquest)
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
