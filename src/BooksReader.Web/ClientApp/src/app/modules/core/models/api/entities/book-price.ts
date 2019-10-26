import { IIdentifiable } from './identifiable';
import { IOwned } from './owned';

export interface BookPrice extends IIdentifiable, IOwned {
    
    bookId: string;
    currencyId: number;

    created: Date,
    price: number;

}
