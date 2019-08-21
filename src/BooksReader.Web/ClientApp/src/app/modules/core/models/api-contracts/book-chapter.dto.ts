export interface BookChapter {
    id: string;
    bookId: string;
    chaperId: string;
    title: string;
    content: string;
    number: number;
    isPublished: boolean;
    created: Date;
}
