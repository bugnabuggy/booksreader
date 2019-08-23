import { IOwned, IIdentifiable } from '../../site/generics';

export interface Book extends IIdentifiable, IOwned {
    title: string;
    author: string;
    description: string;

    isPublished: boolean;
    isForSale: boolean;
    
    picture?: string;

    created: string;
    published?: string;

    ownerName?: string;
    ownerUserName?: string;
}
