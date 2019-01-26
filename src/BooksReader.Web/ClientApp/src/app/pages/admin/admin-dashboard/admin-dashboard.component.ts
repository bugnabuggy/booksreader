import { Component, OnInit } from '@angular/core';
import { AdminUsersService } from '../../../services';
import { WebResult, AppUser } from '../../../models';

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
    this.adminUsers.getUsers().subscribe((val: WebResult<AppUser>) => {
      debugger;
      this.users = val.data;
    });
  }

}
