import { Component, OnInit } from '@angular/core';
import { UserService, NotificationService } from '@br/core/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ChangePasswordDialogComponent } from '../../components/';
import { AppUser } from '@br/core/models';
import { finalize } from 'rxjs/operators';
import { StringConstants } from '@br/config';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  
  profileForm: FormGroup;
  isUiBlocked = false;

  constructor(
    public userSvc: UserService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private notifications: NotificationService
  ) { 
  }

  ngOnInit() {
    let user = this.userSvc.user;

    this.profileForm = this.fb.group({
      avatar: [user.avatar],
      username: [user.username],
      name: [user.name, [Validators.required, Validators.maxLength(200)]],
      email: [user.email, [Validators.required, Validators.email]],
      roles: user.roles.reduce((a, x)=> a + `, ${x}`)
    })    
  }


  updateProfile() {
    this.isUiBlocked = true;
    console.log(this.profileForm.value)
    this.userSvc.updateProfile(this.profileForm.value as AppUser)
      .pipe(finalize(()=>{
        this.isUiBlocked = false;
      }))
      .subscribe(()=>{
        this.notifications.showSuccess(StringConstants.profileUpdated);
      }, err => {
        debugger;
        this.notifications.showError(err.name);
      })
  }

  changePass(e: Event){
    e.cancelBubble = true;
    e.preventDefault();
    console.log('show password change dialog');

    const dialogRef = this.dialog.open(ChangePasswordDialogComponent, {
      minHeight:"50%",
      data: {profile: this.profileForm}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }

  becomeAnAuthor(e: Event){
    e.cancelBubble = true;
    e.preventDefault();
    console.log('become an author redirect');
  }
}
