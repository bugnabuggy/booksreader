export interface BookChapterRequest {
    id: string;
    title: string;
    description?: string;
    isPublished?: boolean;
    content: string;
}
