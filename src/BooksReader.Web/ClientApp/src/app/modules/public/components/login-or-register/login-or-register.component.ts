import { Component, OnInit } from '@angular/core';
import { Endpoints } from '@br/config';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login-or-register',
  templateUrl: './login-or-register.component.html',
  styleUrls: ['./login-or-register.component.scss']
})
export class LoginOrRegisterComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<LoginOrRegisterComponent>,
  ) { }

  Urls = Endpoints.frontend;

  ngOnInit() {
  }

  close() {
    this.dialogRef.close();
  }

}
