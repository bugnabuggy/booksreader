import { Component, OnInit, OnDestroy } from '@angular/core';
import { AdminUsersService, NotificationService } from '@br/core/services';
import { Subscription } from 'rxjs';
import { AppUser, WebResult, OperationResult } from '@br/core/models';
import { PageEvent } from '@angular/material/paginator';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.scss']
})
export class AllUsersComponent implements OnInit, OnDestroy {
  usersSub: Subscription;
  users: AppUser[] = [];
  pageEvent: PageEvent;

  displayedColumns: string[] = ['username', 'name', 'roles', 'actions'];

  constructor(
    public adminUsers: AdminUsersService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.refresh();
  }

  ngOnDestroy() {
    this.usersSub.unsubscribe();
  }

  refresh(){
    if(this.usersSub){
      this.usersSub.unsubscribe();
    }
    
    this.usersSub = this.adminUsers.getUsers().subscribe((val: WebResult<AppUser[]>) => {
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

  delete(user: AppUser) {
    this.adminUsers.delete(user.username)
    .subscribe( val => {
      this.users = this.users.filter(x=>x.username !== user.username);
        this.notifications.showInfo(SiteMessages.user.userDeleted);
    });
  }

}
