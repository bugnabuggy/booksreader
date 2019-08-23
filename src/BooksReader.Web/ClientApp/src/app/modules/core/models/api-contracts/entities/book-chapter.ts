import { IIdentifiable, IOwned } from '../../site/generics';

export interface BookChapter extends IIdentifiable, IOwned {
    
    bookId: string;
    number: number;

    title: string;
    version: number;

    content: string;
    
    isPublished: boolean;
    created: Date;
}
