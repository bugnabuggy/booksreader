import { BookPrice } from '../../entities';
import { SubscriptionStatus } from '@br/core/enums/subscription-status';
import { SiteConstants } from '@br/config';

export interface BookMarketDto {
    bookId: string;
    semanticUrl?: string;
    title: string;
    author: string;
    authorId: string;
    authorSemanticUrl?: string; 
    description: string;
    picture: string;
    isForSale: boolean;
    published: Date;
    subscriptionDurationDays?: number


    bookPrices: BookPrice[];
    subscription: SubscriptionStatus;
}

export const EmptyBookMarketDto: BookMarketDto = {
    bookId: '',
    author:'',
    authorId: '',
    description:'',
    title:'',
    isForSale: false,
    picture: SiteConstants.defaultImage,
    published: null,
    subscription: SubscriptionStatus.none,
    bookPrices: []
};

