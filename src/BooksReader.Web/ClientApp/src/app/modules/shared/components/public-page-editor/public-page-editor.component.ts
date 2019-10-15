import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { UserDomain, PublicPage } from '@br/core/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { ConfirmationType, ConfirmationResult, PublicPageType } from '@br/core/enums';
import { PublicPagesService, NotificationService } from '@br/core/services';
import { ClearNullValues } from '@br/utilities/clear-null-values';
import { finalize } from 'rxjs/operators';
import { getErrorMessage } from '@br/utilities/error-extractor';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-public-page-editor',
  templateUrl: './public-page-editor.component.html',
  styleUrls: ['./public-page-editor.component.scss']
})
export class PublicPageEditorComponent implements OnInit, OnChanges {
 
  @Input() domains: UserDomain[];
  domainsForSelect: UserDomain[] = [];

  @Input() page: PublicPage;
  @Input() pageType: PublicPageType;
  @Input() subjectId: string;

  showHelp = false;
  uiIsBlocked = false;

  pageForm: FormGroup = this.fb.group({
    domainId: [''],
    content: ['']
  });

  constructor (
    private fb: FormBuilder,
    private dialog: MatDialog,
    private publicPagesSvc: PublicPagesService,
    private notifications: NotificationService
  ) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.page) {
      this.initForm();
    }

    this.domainsForSelect = [...this.domains].filter(x=>x.id);
  }

  add() {
    this.page = {} as PublicPage;
    this.initForm();
  }

  delete() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        title:'Please confirm',
        question:`Are you sure that you wnat to delete public page?`,
        type: ConfirmationType.yesNo
      }
    });

    dialogRef.afterClosed().subscribe((result: ConfirmationResult) => {
      if (result == ConfirmationResult.yes) {
        
        this.publicPagesSvc.delete(this.page.id)
        .subscribe( val => {
          this.page = null;
          this.notifications.showSuccess(SiteMessages.publicPages.deleted)
        }, err => {
          this.notifications.showError(err);
        })
      }
      
    });
  }

  initForm() {
    this.pageForm = this.fb.group({
      id: [this.page.id],
      subjectId: [this.page.subjectId],
      seoInfoId: [this.page.seoInfoId],

      domainId: [this.page.domainId, Validators.required],
      urlPath: [this.page.urlPath],
      content: [this.page.content, Validators.required]
    });
  }

  submit() {
    debugger;
    let form = this.pageForm.value as PublicPage;
    
    form.pageType = this.pageType;
    
    form = ClearNullValues(form);

    const observable = form.id
    ? this.publicPagesSvc.update(form)
    : this.publicPagesSvc.add(form)
    
    this.uiIsBlocked = true;
    observable.pipe(
      finalize(()=>{
        this.uiIsBlocked = false;
      })
    ).subscribe((val) => {
      this.notifications.showSuccess(SiteMessages.publicPages.added);
    }, (err) => {
      this.notifications.showError(err);
    })
  }
}
