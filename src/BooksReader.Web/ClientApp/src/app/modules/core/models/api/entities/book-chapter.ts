import { IIdentifiable } from './identifiable';
import { IOwned } from './owned';

export interface BookChapter extends IIdentifiable, IOwned {

    bookId: string;
    number: number;
    title: string;
    description: string;

    version: number;
    content: string;

    created: Date;
    isPublished: boolean;
}
