import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { passwordConfirmationValidator } from '@br/utilities/validators';
import { SecurityService, UserService } from '@br/core/services';
import { RegistrationRequest } from '@br/core/models';
import { finalize, flatMap } from 'rxjs/operators';
import { NotificationService } from '@br/core/services/notification.service';
import { getErrorMessage } from '@br/utilities/error-extractor';
import { SiteMessages } from '@br/config/site-messages';
import { TranslateService } from '@ngx-translate/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  uiIsBlocked = false;
  serverMessage = '';

  public readonly regForm = this.fb.group({
    username: ['', Validators.required],
    fullname: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(3)]],
    confirmation: ['', [Validators.required, Validators.minLength(3)]],
    agree: [''],
  }, { validators: passwordConfirmationValidator })

  indeterminate: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userSvc: UserService,
    private notifications: NotificationService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
  }

  registration() {
    const data = this.regForm.value as RegistrationRequest;

    this.uiIsBlocked = true;
    this.userSvc.registration(data)
      .pipe(
        finalize(() => { this.uiIsBlocked = false; }),
        flatMap(() => this.translate.get(SiteMessages.user.registration.success)))
      .subscribe(val => {
        this.notifications.showSuccess(val);
      }, (err: HttpErrorResponse) => {
        if (err.status == 400) {
          this.serverMessage = err.error;
        }

        const msg = getErrorMessage(err);
        this.notifications.showError(msg);
      })

  }

}
