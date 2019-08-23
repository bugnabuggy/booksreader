import { IIdentifiable, IOwned } from '../../site/generics';

export interface BookPrice extends IIdentifiable, IOwned {
    
    price: number;
    created: Date;
    
    bookId: string;
    currencyId: string;
}
