import { IOwned, IIdentifiable } from '../site/generics';

export interface Book extends IIdentifiable, IOwned {
    title: string;
    author: string;
    description: string;

    isPublished: boolean;
    created: string;
    published: string;

    ownerName?: string;
    ownerUserName?: string;
}
