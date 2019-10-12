import { Component, OnInit, Input } from '@angular/core';
import { UserDomain, Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';
import { UserDomainsService, NotificationService } from '@br/core/services';
import { getErrorMessage } from '@br/utilities/error-extractor';
import { finalize } from 'rxjs/operators';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-domains-list',
  templateUrl: './domains-list.component.html',
  styleUrls: ['./domains-list.component.scss']
})
export class DomainsListComponent {

  @Input() domains: UserDomain[];

  editItem = {

  } as UserDomain;

  uiIsBlocked = false;
  addInProgress = false;

  actions = {
    [ActionType.select]: this.select,
    [ActionType.edit]: this.save,
    [ActionType.delete]: this.delete,
  }

  constructor(
    private notifications: NotificationService,
    private domainsSvc: UserDomainsService
  ) { }

  addDomain() {
    this.addInProgress = true;
    this.domains.push({} as UserDomain);
  }

  select(domain: UserDomain) {
    this.editItem = this.editItem.id == domain.id
      ? {} as UserDomain
      : { ...domain };

    // if it was addition operation
    if (!domain.id) {
      this.addInProgress = false;
      this.domains.pop()
    }
  }

  save(domain: UserDomain) {
    this.uiIsBlocked = true;
    debugger;
    if (domain.id) {
      this.domainsSvc.update(domain)
        .pipe(finalize(() => {
          this.uiIsBlocked = false;
        }))
        .subscribe(
          val => {
            this.addInProgress = false;
            this.editItem = {} as UserDomain;
          },
          err => {
            const msg = getErrorMessage(err);
            this.notifications.showError(msg);
          }
        );
    } else {
      this.domainsSvc.add(domain)
        .pipe(finalize(() => {
          this.uiIsBlocked = false;
        }))
        .subscribe(
          val => {
            domain = val.data;
            this.addInProgress = false;
            this.editItem = {} as UserDomain;
          },
          err => {
            const msg = getErrorMessage(err);
            this.notifications.showError(msg);
          }
        );
    }


  }

  delete(domain: UserDomain) {
    this.uiIsBlocked = true;
    this.domainsSvc.delete(domain.id)
      .pipe(finalize(()=>{
        this.uiIsBlocked = false;
        
      }))
      .subscribe(val => {
        let index = this.domains.indexOf(domain);
        this.domains.splice(index,1);
        this.notifications.showSuccess(SiteMessages.domains.deleted);
      }, err => {
        const msg = getErrorMessage(err);
        this.notifications.showError(msg);
      });
  }

  doAction(action: Action<UserDomain>) {
    let func = this.actions[action.action];
    if (func && typeof func == 'function') {
      func.apply(this, [action.data]);
    }
  }
}
