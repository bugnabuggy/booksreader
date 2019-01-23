import { Component } from '@angular/core';
import { SecurityService } from '../../services/';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
  constructor(
    public security: SecurityService
  ) {
  }
}
