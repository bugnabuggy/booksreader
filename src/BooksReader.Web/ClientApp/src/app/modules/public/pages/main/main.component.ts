import { Component, OnInit } from '@angular/core';
import { SecurityService, UserService } from '@br/core/services';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  public Endpoints = Endpoints;


  constructor(
    public security: SecurityService,
    public userSvc: UserService) { }

  ngOnInit() {
  }

}
