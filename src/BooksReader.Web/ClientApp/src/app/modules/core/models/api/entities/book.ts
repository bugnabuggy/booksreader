import { IIdentifiable } from './identifiable'
import { IOwned } from './owned';

export interface Book extends IIdentifiable, IOwned {
        
        title: string;
        author: string;
        description: string;
        isPublished: boolean;
        isForSale: boolean;
        picture: string;
        created: Date;
        published?: Date;
}
