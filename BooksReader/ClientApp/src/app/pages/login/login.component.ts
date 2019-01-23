import { Component, DoCheck } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import {
  AuthorizationService,
  SecurityService,
  NotificationService
} from '../../services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements DoCheck  {
  userNameFormControl = new FormControl('', [
    Validators.required
]);

passwordFormControl = new FormControl('', [
    Validators.required
]);

ngDoCheck(): void {
    this.security.isLoggedIn = false;
}

constructor(
    private security: SecurityService,
    private authorization: AuthorizationService,
    private notifications: NotificationService
) {
    this.security.isLoggedIn = false;
}

login(e) {
    e.preventDefault();

    this.authorization.logIn(this.userNameFormControl.value, this.passwordFormControl.value)
        .subscribe((data) => {

        }, (err) => {
            if (err.status === 400) {
                this.notifications.showError(`Invalid login or password`);
            }
        });
}
}
