import { IIdentifiable } from './identifiable';
import { PublicPageType } from '@br/core/enums';

export interface PublicPage extends IIdentifiable {
    id: string;

    pageType: PublicPageType;
    subjectId?: string;
    domainId: string;
    seoInfoId?: string;

    urlPath: string;
    content: string;
}