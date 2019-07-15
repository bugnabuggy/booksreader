import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-buy-button',
  templateUrl: './buy-button.component.html',
  styleUrls: ['./buy-button.component.scss']
})
export class BuyButtonComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  click() {
    alert('buy buy baby buy buy!');
  }
}
