import { Component, OnInit } from '@angular/core';
import { Endpoints } from '@br/config';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  Urls = Endpoints.frontend;

  constructor() { }

  ngOnInit() {
  }

}
