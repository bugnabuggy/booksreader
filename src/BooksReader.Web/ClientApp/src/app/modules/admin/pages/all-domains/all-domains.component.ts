import { Component, OnInit } from '@angular/core';
import { UserDomain, StandardFilters, UserDomainState } from '@br/core/models';
import { UserDomainsService, NotificationService } from '@br/core/services';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { DomainVerificationTypeStrings } from '@br/core/enums';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-all-domains',
  templateUrl: './all-domains.component.html',
  styleUrls: ['./all-domains.component.scss']
})
export class AllDomainsComponent implements OnInit {


  domains: UserDomainState[]
  filters = {
    pageSize: 20,
    orderByField: 'domainName'
  } as StandardFilters;
  pageEvent: PageEvent;
  totalRecords = 0;
  
  
  DomainVerificationTypeStrings = DomainVerificationTypeStrings;
  columns = [
    'domainName', 
    'username', 
    'type', 
    'verified',
    'numberOfPages',
    'actions'
  ];

  constructor(
    private domainsSvc: UserDomainsService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
    this.refresh(this.filters);
  }

  pageChanged(event){
    this.filters.pageSize = event.pageSize;
    this.filters.pageNumber = event.pageIndex + 1;
    this.refresh(this.filters);
  }

  refresh(filters: StandardFilters) {
    this.domainsSvc.list(filters)
    .subscribe( val => {
      this.totalRecords = val.total;
      this.domains = val.data as any;
    }, 
     err => {
      this.notifications.showError(err);
     })
  }

  toggle(domainState: UserDomainState) {
    this.domainsSvc.toggleState(domainState)
      .subscribe((val) => {
        let domain = this.domains.find(x=>x.domainId == val.data.id);
        domain.verified = val.data.verified;
        this.notifications.showSuccess(SiteMessages.domains.verificationToggled)
      }, err => {
        this.notifications.showError(err);
      });
    
  }
}
