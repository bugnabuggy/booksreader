import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { UserDomain, PublicPage } from '@br/core/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { ConfirmationType, ConfirmationResult, PublicPageType, PublicPageTypeStrings } from '@br/core/enums';
import { PublicPagesService, NotificationService } from '@br/core/services';
import { ClearNullValues } from '@br/utilities/clear-null-values';
import { finalize } from 'rxjs/operators';
import { SiteMessages } from '@br/config/site-messages';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';



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

  pageTypeString: string;

  showHelp = false;
  uiIsBlocked = false;

  pageForm: FormGroup = this.fb.group({
    domainId: [''],
    content: ['']
  });

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private publicPagesSvc: PublicPagesService,
    private notifications: NotificationService, 
    private translateSvc: TranslateService
  ) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.page) {
      this.initForm();
    }

    this.translateSvc.get(PublicPageTypeStrings[PublicPageType.bookPage] || '')
      .subscribe(val => {
        this.pageTypeString = val; 
      });
    
    this.domainsForSelect = [...this.domains].filter(x => x.id);
  }

  add() {
    this.page = {
      pageType: this.pageType,
      subjectId: this.subjectId
    } as PublicPage;
    this.initForm();
  }

  delete() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        title: 'Please confirm',
        question: `Are you sure that you wnat to delete public page?`,
        type: ConfirmationType.yesNo
      }
    });

    dialogRef.afterClosed().subscribe((result: ConfirmationResult) => {
      if (result == ConfirmationResult.yes) {

        if (this.page.id) {
          this.publicPagesSvc.delete(this.page.id)
            .subscribe(val => {
              this.page = null;
              this.notifications.showSuccess(SiteMessages.publicPages.deleted)
            }, err => {
              this.notifications.showError(err);
            })
        } else {
          this.page = null;
        }

      }

    });
  }

  initForm() {
    this.pageForm = this.fb.group({
      id: [this.page.id],
      subjectId: [this.page.subjectId],
      pageType: [this.page.pageType],
      seoInfoId: [this.page.seoInfoId],
      
      domainId: [this.page.domainId, Validators.required],
      urlPath: [this.page.urlPath],
      content: [this.page.content, Validators.required]
    });
  }

  onKey($event: KeyboardEvent)  {
    let charCode = String.fromCharCode($event.which).toLowerCase();
    if ($event.ctrlKey && charCode === 's') {
      $event.preventDefault();
      $event.cancelBubble = true;

      if(this.pageForm.valid) {
        this.submit();
      }
    }
  }

  submit() {
    let form = this.pageForm.value as PublicPage;

    form = ClearNullValues(form);

    const observable = form.id
      ? this.publicPagesSvc.update(form)
      : this.publicPagesSvc.add(form)

    this.uiIsBlocked = true;
    observable.pipe(
      finalize(() => {
        this.uiIsBlocked = false;
      })
    ).subscribe((val) => {

      // update passed page
      this.page = val.data;
      this.pageForm.patchValue({id: val.data.id});

      let msg = form.id 
      ? SiteMessages.publicPages.saved
      : SiteMessages.publicPages.added;
      this.notifications.showSuccess(msg);
    }, (err) => {
      this.notifications.showError(err);
    })
  }
}
