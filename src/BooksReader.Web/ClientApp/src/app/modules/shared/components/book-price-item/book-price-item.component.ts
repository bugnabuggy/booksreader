import { Component, OnInit, Input, Output, EventEmitter, OnChanges, OnDestroy } from '@angular/core';
import { BookPrice, Action, TypeValue } from '@br/core/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListsService } from '@br/core/services';
import { Subscription } from 'rxjs';
import { TypeListsIds, ActionType } from '@br/core/enums';

@Component({
  selector: 'app-book-price-item',
  templateUrl: './book-price-item.component.html',
  styleUrls: ['./book-price-item.component.scss']
})
export class BookPriceItemComponent implements OnInit, OnChanges, OnDestroy {

  @Input() price: BookPrice;
  @Input() isEdit: boolean;
  @Input() takenCurrencies: number[] = [];

  @Output() doAction = new EventEmitter<Action<BookPrice>>();

  editForm = new FormGroup({});
  avaliableCurrencies: TypeValue[];
  currencies: TypeValue[];
  listsSub: Subscription


  constructor(
    private fb: FormBuilder,
    private listSvc: ListsService
  ) { 
    this.listsSub = this.listSvc.lists$.subscribe(lists => {
      let currenciesList = lists.find(x => x.id == TypeListsIds.currency);
      this.currencies = currenciesList && currenciesList.values || [];
    });
  }

  ngOnInit() {
    
  }

  ngOnDestroy() {
    this.listsSub.unsubscribe();
  }

  ngOnChanges() {
    if (this.currencies) {
      this.avaliableCurrencies = this.currencies.filter(
        x => this.takenCurrencies.find(y => y == x.id) == null || this.price.currencyId == x.id
          ? true
          : false);
    }
    
    this.editForm = this.fb.group({
      id: [this.price.id],
      ownerId: [this.price.ownerId],
      bookId: [this.price.bookId],

      price: [this.price.price, Validators.required],
      currencyId: [this.price.currencyId, Validators.required]
    });
  }

  close(item: BookPrice) {
    this.doAction.emit({
      action: ActionType.close,
      data: item
    } as Action<BookPrice>);
  }

  save() {
    let data = this.editForm.value;

    this.doAction.emit({
      action: ActionType.save,
      data: data
    } as Action<BookPrice>);
  }

  delete(item: BookPrice) {
    this.doAction.emit({
      action: ActionType.delete,
      data: item
    } as Action<BookPrice>);
  } 

  select(item: BookPrice) {
    this.doAction.emit({
      action: ActionType.select,
      data: item
    } as Action<BookPrice>);
  }
}
