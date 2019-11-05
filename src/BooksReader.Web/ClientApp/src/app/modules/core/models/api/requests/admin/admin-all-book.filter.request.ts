import {  BooksFilters } from '../filters';

export interface AdminAllBooksFilter extends BooksFilters {
    isPublished?: boolean;
    verified?: boolean;

    PublishedFrom? :string; 
    PublishedTo? :string;
   
    CreatedFrom? :string;
    CreatedTo? :string;
}
