import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../services';

@Component({
  selector: 'app-force-logout',
  templateUrl: './force-logout.component.html',
  styleUrls: ['./force-logout.component.scss']
})
export class ForceLogoutComponent implements OnInit {

  constructor(
    public security: SecurityService
  ) { }

  ngOnInit() {
  }

}
