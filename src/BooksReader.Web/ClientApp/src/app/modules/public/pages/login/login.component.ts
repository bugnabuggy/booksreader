import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { UserService } from '@br/core/services';
import { finalize } from 'rxjs/operators';
import { SocialLoginService } from '@br/integrations/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  uiIsBlocked = false;
  errorMessage = '';
  loggedInWithFacebook = false;

  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(3)]],
  })

  constructor(
    private fb: FormBuilder,
    private userSvc: UserService,
    public socialSvc: SocialLoginService,
  ) { }

  ngOnInit() {
  }

  login() {
    const data = this.loginForm.value;
    this.uiIsBlocked = true;

    this.userSvc.logIn(data.username, data.password)
      .pipe(finalize(() => {
        this.uiIsBlocked = false;
      }))
      .subscribe(val => {

      }, (err) => {
        this.errorMessage = err.message;
      })
  }

  socialLogin() {
    this.socialSvc.signInWithFB().subscribe(user => {
      this.loggedInWithFacebook = true;
      this.userSvc.externalLogIn('Facebook', user.authToken)
        .subscribe(val => {
          console.log(val);
          // redirect or ask for protected endpoint
        });
      console.log(user);
    });
  }

  socialLogout() {
    this.socialSvc.signOutWithFB().subscribe(val => {
      this.loggedInWithFacebook = false;
      console.log(val);
    });
  }
}
