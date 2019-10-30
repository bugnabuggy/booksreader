import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserDomain, Action } from '@br/core/models';
import { ActionType, ConfirmationResult, ConfirmationType } from '@br/core/enums';
import { UserDomainsService, NotificationService } from '@br/core/services';
import { getErrorMessage } from '@br/utilities/error-extractor';
import { finalize } from 'rxjs/operators';
import { SiteMessages } from '@br/config/site-messages';
import { TranslateService } from '@ngx-translate/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '@br/controls/dialogs';


@Component({
  selector: 'app-domains-list',
  templateUrl: './domains-list.component.html',
  styleUrls: ['./domains-list.component.scss']
})
export class DomainsListComponent {

  @Input() domains: UserDomain[];
  @Output() modified = new EventEmitter<UserDomain[]>();


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
    private domainsSvc: UserDomainsService,
    private translate: TranslateService,
    private dialog: MatDialog,
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
    if (!domain.id) {
      delete domain.id
    }

    if (!domain.ownerId) {
      delete domain.ownerId
    }

    this.uiIsBlocked = true;
    const observable = domain.id
      ? this.domainsSvc.update(domain)
      : this.domainsSvc.add(domain);

    observable
      .pipe(finalize(() => {
        this.uiIsBlocked = false;
      }))
      .subscribe(
        val => {
          let index = this.domains.findIndex(x => x.id == domain.id);
          this.domains[index] = val.data;
          this.modified.emit(this.domains);

          this.addInProgress = false;
          this.editItem = {} as UserDomain;

          this.translate.get(SiteMessages.domains.saved)
            .subscribe(val => {
              this.notifications.showSuccess(val);
            })

        },
        err => {
          const msg = getErrorMessage(err);
          this.notifications.showError(msg);
        }
      );
  }

  delete(domain: UserDomain) {
    this.uiIsBlocked = true;

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        title:'Please confirm',
        text:`Are you sure that you wnat to delete domain?`,
        type: ConfirmationType.yesNo
      }
    });

    dialogRef.afterClosed().subscribe((val: ConfirmationResult) => {
      if (val == ConfirmationResult.yes) {
        this.domainsSvc.delete(domain.id)
          .pipe(finalize(() => {
            this.uiIsBlocked = false;

          }))
          .subscribe(val => {
            let index = this.domains.indexOf(domain);
            this.domains.splice(index, 1);
            this.modified.emit(this.domains);

            this.notifications.showSuccess(SiteMessages.domains.deleted);
          }, err => {
            const msg = getErrorMessage(err);
            this.notifications.showError(msg);
          });
      }
    })


  }

  doAction(action: Action<UserDomain>) {
    let func = this.actions[action.action];
    if (func && typeof func == 'function') {
      func.apply(this, [action.data]);
    }
  }
}
