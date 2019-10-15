import { Component, OnInit } from '@angular/core';
import { UserDomain, StandardFilters } from '@br/core/models';
import { UserDomainsService } from '@br/core/services';

@Component({
  selector: 'app-all-domains',
  templateUrl: './all-domains.component.html',
  styleUrls: ['./all-domains.component.scss']
})
export class AllDomainsComponent implements OnInit {


  domains: UserDomain[]
  filters = {

  } as StandardFilters;

  columns = [
    'name', 
    'username', 
    'type', 
    'status', 
    'actions'
  ];

  constructor(
    private domainsSvc: UserDomainsService
  ) { }

  ngOnInit() {
    this.refresh
  }

  refresh() {
    this.domainsSvc.list(this.filters);
  }

}
