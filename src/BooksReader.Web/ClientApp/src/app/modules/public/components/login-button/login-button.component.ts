import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Endpoints } from '@br/config';
import { UserService } from '@br/core/services';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss']
})
export class LoginButtonComponent implements OnInit {

  constructor(
    private router: Router,
    private userSvc: UserService

    ) { }

  ngOnInit() {
  }

  login(){
    this.router.navigateByUrl(Endpoints.forntend.authorization);
    this.userSvc.showUi();
  }

}
