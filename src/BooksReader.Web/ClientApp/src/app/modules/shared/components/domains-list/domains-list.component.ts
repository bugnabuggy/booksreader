import { Component, OnInit, Input } from '@angular/core';
import { UserDomain, Action } from '@br/core/models';
import { ActionType } from '@br/core/enums';

@Component({
  selector: 'app-domains-list',
  templateUrl: './domains-list.component.html',
  styleUrls: ['./domains-list.component.scss']
})
export class DomainsListComponent  {

  @Input() domains: UserDomain[];
  
  editItem = {
    
  } as UserDomain;
  
  uiIsBlocked = false;
  addInProgress = false;

  actions = {
      [ActionType.select] : this.select,
      [ActionType.edit] : this.save,
      [ActionType.delete] : this.delete,
  }

  constructor() { }

  addDomain() {
    this.addInProgress = true;
    this.domains.push({} as UserDomain);
  }

  select(domain: UserDomain) {
    this.editItem = this.editItem.id == domain.id 
      ? {} as UserDomain
      : {...domain};

      // if it was addition operation
      if(!domain.id){
        this.addInProgress = false;
        this.domains.pop()
      }
  }

  save(domain: UserDomain) {
    this.addInProgress = false;
    this.editItem = {} as UserDomain;
  }

  delete(domain: UserDomain) {

  }

  doAction(action: Action<UserDomain> ) {
    console.log(action);
    let func = this.actions[action.action];
    if (func && typeof func == 'function') {
      func.apply(this, [action.data]);
    }
  }
}
