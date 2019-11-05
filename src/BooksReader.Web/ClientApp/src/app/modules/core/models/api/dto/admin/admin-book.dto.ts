export interface AdminBookDto {
    bookId: string;
    bookTitle: string ;
    username: string ;
    created: Date;
    publishd?: Date; 
    isPublished: boolean;
    verified: boolean;
}
