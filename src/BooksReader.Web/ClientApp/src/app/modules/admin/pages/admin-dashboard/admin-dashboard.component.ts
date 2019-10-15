import { Component, OnInit } from '@angular/core';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  constructor() { }
  Urls = Endpoints.frontend.admin;

  ngOnInit() {
  }

}
