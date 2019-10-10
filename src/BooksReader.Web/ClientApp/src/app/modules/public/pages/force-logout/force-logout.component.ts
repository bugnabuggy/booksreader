import { Component, OnInit } from '@angular/core';
import { UserService } from '@br/core/services';

@Component({
  selector: 'app-force-logout',
  templateUrl: './force-logout.component.html',
  styleUrls: ['./force-logout.component.scss']
})
export class ForceLogoutComponent implements OnInit {

  constructor(
    public userSvc: UserService
  ) { }

  ngOnInit() {

  }

}
