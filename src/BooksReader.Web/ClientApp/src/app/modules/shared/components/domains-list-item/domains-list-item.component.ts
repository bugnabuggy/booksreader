import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserDomain } from '@br/core/models';
import { DomainVerificationType, ActionType } from '@br/core/enums';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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

  domainForm: FormGroup; 

  constructor(
    private fb: FormBuilder
  ) { 

  }

  ngOnInit() {
    this.domainForm = this.fb.group({
      id: [this.domain.id],
      name: [this.domain.name, [Validators.required]],
      protocol: [this.domain.protocol,  Validators.required],
      verificationType: [this.domain.verificationType, Validators.required]
    });
  }
}
