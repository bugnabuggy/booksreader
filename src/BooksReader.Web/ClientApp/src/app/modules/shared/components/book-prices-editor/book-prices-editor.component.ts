import { Component, OnInit, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { BookPrice, Action, ConfirmationDialogModel, OperationResult, Book } from '@br/core/models';
import { ActionType, ConfirmationType, ConfirmationResult } from '@br/core/enums';
import { BooksPricesService, NotificationService } from '@br/core/services';
import { finalize } from 'rxjs/operators';
import { ClearNullValues } from '@br/utilities/clear-null-values';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { SiteMessages } from '@br/config/site-messages';

@Component({
  selector: 'app-book-prices-editor',
  templateUrl: './book-prices-editor.component.html',
  styleUrls: ['./book-prices-editor.component.scss']
})
export class BookPricesEditorComponent implements OnInit, OnChanges{

  @Input() prices: BookPrice[]
  @Input() bookId: string;
  @Output() updated = new EventEmitter<BookPrice[]>();


  editItem: BookPrice = {} as BookPrice;
  takenCurrencies: number[];
  addInProgress = false;
  uiIsBlocked = false;

  actions = {};

  constructor (  
    private bookPriceSvc: BooksPricesService,
    private notifications: NotificationService,
    private dialog: MatDialog
  ) { 
    this.actions[ActionType.close] = this.cancelEdit;
    this.actions[ActionType.save] = this.save;
    this.actions[ActionType.select] = this.select;
    this.actions[ActionType.delete] = this.delete;
  }

  ngOnInit() {
  }

  ngOnChanges() {
    this.takenCurrencies = this.prices && this.prices.map(x => x.currencyId) || [];
  }

  addNew() {
    this.addInProgress = true;
    this.editItem = {
      bookId: this.bookId
    } as BookPrice;
    
    this.prices.push(this.editItem);
  }

  doAction(action: Action<BookPrice>) {
    let actionFunc = this.actions[action.action];

    if(actionFunc){
      actionFunc = actionFunc.bind(this);
      actionFunc(action.data);
    }
  }

  cancelEdit(data) {
    this.editItem = {
      bookId: this.bookId
    } as BookPrice;
    
    if (this.addInProgress) {
      var index = this.prices.findIndex(x => !x.id);
      this.prices.splice(index, 1);
      this.addInProgress = false;
    }
  }

  select(item: BookPrice) {
    this.editItem = item;
  }

  save(item: BookPrice) {
    debugger;

    item = ClearNullValues(item);

    const observable = item.id
      ? this.bookPriceSvc.update(item)
      : this.bookPriceSvc.add(item);
    
    observable.pipe(
      finalize(()=>{
        this.uiIsBlocked = false;
        
      })
    ).subscribe( val => {
      
      if(item.id) {
        var index = this.prices.findIndex(x=>x.id == item.id);
        this.prices[index] = val.data;
        this.notifications.showSuccess(SiteMessages.author.prices.edited);
      } else {
        this.prices[this.prices.length - 1] = val.data;
        this.addInProgress = false;
        this.notifications.showSuccess(SiteMessages.author.prices.added);
      }

      this.takenCurrencies = this.prices && this.prices.map(x => x.currencyId) || [];
      
      // update prices with new reference
      this.updated.emit(this.prices.map(x => x));

      // reset edited item
      this.editItem = {
        bookId: this.bookId
      } as BookPrice;

    }, err => {
      this.notifications.showError(err);
    });
  }

  delete(item: BookPrice) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      minHeight: "50%",
      data: {
        type: ConfirmationType.yesNo,
        question: SiteMessages.author.prices.deleteQuestion
      } as ConfirmationDialogModel
    });

    dialogRef.afterClosed().subscribe(confirmresult  => {
      if (confirmresult == ConfirmationResult.yes) {
        this.bookPriceSvc.delete(item.id)
          .subscribe((val: OperationResult<BookPrice>) => {
            let index = this.prices.findIndex(x => x.id == item.id);
            this.prices.splice(index, 1);
            this.takenCurrencies = this.prices && this.prices.map(x => x.currencyId) || [];

            // update prices with new reference
            this.updated.emit(this.prices.map(x => x));

            this.notifications.showSuccess(SiteMessages.author.prices.deleted)
          });
      }
    });
  }
}
