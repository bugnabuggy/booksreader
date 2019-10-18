import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Endpoints } from '@br/config';
import { UserService } from '@br/core/services';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss']
})
export class LoginButtonComponent implements OnInit {

  @Input() class = '';
  @Input() text;

  constructor(
    private router: Router,
    private userSvc: UserService
  ) { }

  ngOnInit() {
  }

  login() {
    this.userSvc.toggleUi(true);
    this.router.navigateByUrl(Endpoints.frontend.authorizationUrl);
  }
}
