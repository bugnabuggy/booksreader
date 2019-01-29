import { Component, OnInit } from '@angular/core';
import { AdminUsersService } from '../../../services';
import { WebResult, AppUser, OperationResult } from '../../../models';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {
  users: AppUser[];
  displayedColumns: string[] = ['username', 'name', 'roles', 'actions'];

  constructor(
    public adminUsers: AdminUsersService
  ) { }

  ngOnInit() {
    this.adminUsers.getUsers().subscribe((val: WebResult<AppUser[]>) => {
      this.users = val.data;
    });
  }

  toggle(element: AppUser, role: string) {
    if (element.roles.findIndex(x => x === role) > -1) {
      this.removeFromRole(element, role);
    } else {
      this.addToRole(element, role);
    }

  }

  addToRole(element: AppUser, role: string) {
    this.adminUsers.addUserToRole(element.username, role)
      .subscribe((val: OperationResult<any>) => {
        if (val.success) {
          element.roles.push(role);
        } else {
          console.error(val.messages);
        }
      });
  }

  removeFromRole(element: AppUser, role: string) {
    this.adminUsers.removeUserFromRole(element.username, role)
      .subscribe(val => {
        const index = element.roles.findIndex(x => x === role);
        element.roles.splice(index, 1);
      });
  }
}
