import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserDomain } from '@br/core/models';
import { DomainVerificationType, DomainVerificationTypeStrings } from '@br/core/enums';
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
  DomainVerificationTypeStrings = DomainVerificationTypeStrings;

  domainForm: FormGroup; 

  constructor(
    private fb: FormBuilder
  ) { 

  }

  ngOnInit() {
    this.domainForm = this.fb.group({
      id: [this.domain.id],
      ownerId:[this.domain.ownerId],
      name: [this.domain.name, [Validators.required]],
      protocol: [this.domain.protocol,  Validators.required],
      verificationType: [this.domain.verificationType, Validators.required],
      certificate: [this.domain.certificate]
    });
  }
}
