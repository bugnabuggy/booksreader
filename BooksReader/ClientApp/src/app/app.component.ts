import { Component, OnInit } from '@angular/core';
import { SecurityService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(
    private security: SecurityService
  ) {}

  ngOnInit() {
    this.security.init();
  }
}
