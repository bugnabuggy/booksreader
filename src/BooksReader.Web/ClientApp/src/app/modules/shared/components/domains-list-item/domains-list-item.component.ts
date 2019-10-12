import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserDomain } from '@br/core/models';
import { DomainVerificationType } from '@br/core/enums';

@Component({
  selector: 'app-domains-list-item',
  templateUrl: './domains-list-item.component.html',
  styleUrls: ['./domains-list-item.component.scss']
})
export class DomainsListItemComponent implements OnInit {
  @Input() domain: UserDomain;
  @Input() isEdit: boolean;

  @Output() action = new EventEmitter<any>()

  DomainVerificationType = DomainVerificationType;

  constructor() { }

  ngOnInit() {
  }
}
