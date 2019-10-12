import { Component, OnInit } from '@angular/core';
import { UserService, SecurityService } from '@br/core/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from '@br/core/services/notification.service';
import { Endpoints } from '@br/config';
import { ChangePasswordDialogComponent } from '../../components';
import { AppUser } from '@br/core/models';
import { finalize } from 'rxjs/operators';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  Endpoints = Endpoints;
  profileForm: FormGroup;
  uiIsBlocked = false;

  constructor(
    public userSvc: UserService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    let user = this.userSvc.user;

    this.profileForm = this.fb.group({
      avatar: [user.avatar],
      username: [user.username],
      name: [user.name, [Validators.required, Validators.maxLength(200)]],
      email: [user.email, [Validators.required, Validators.email]],
      roles: user.roles.length > 0 ? user.roles.reduce((a, x)=> a + `, ${x}` ) : ''
    })    
  }

  updateProfile() {
    this.uiIsBlocked = true;
    console.log(this.profileForm.value)
    this.userSvc.updateProfile(this.profileForm.value as AppUser)
      .pipe(finalize(()=>{
        this.uiIsBlocked = false;
      }))
      .subscribe(()=>{
        this.notifications.showSuccess(SiteMessages.user.profile.updated);
      }, err => {
        debugger;
        this.notifications.showError(err.name);
      })
  }

  changePass(e: Event){
    e.cancelBubble = true;
    e.preventDefault();

    const dialogRef = this.dialog.open(ChangePasswordDialogComponent, {
      minHeight:"50%",
      data: {profile: this.profileForm}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    });
  }
}
