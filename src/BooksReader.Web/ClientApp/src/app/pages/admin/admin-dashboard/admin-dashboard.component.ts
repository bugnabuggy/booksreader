import { Component, OnInit } from '@angular/core';
import { AdminUsersService } from '../../../services';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {
  users: any;


  constructor(
    public adminUsers: AdminUsersService
  ) { }

  ngOnInit() {
    this.adminUsers.getUsers().subscribe(val => {
      this.users = val;
    });
  }

}
