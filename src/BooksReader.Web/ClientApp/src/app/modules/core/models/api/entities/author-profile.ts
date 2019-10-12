import { IIdentifiable } from './identifiable';

export interface AuthorProfile extends IIdentifiable {
    id: string;
    userId: string;
    authorName: string;
    description: string;
}
