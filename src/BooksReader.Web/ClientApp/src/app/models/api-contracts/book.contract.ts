export interface Book {
    id: string;
    ownerId: string;

    title: string;
    author: string;

    created: string;
    published: string;

    ownerName?: string;
    ownerUserName?: string;
}
