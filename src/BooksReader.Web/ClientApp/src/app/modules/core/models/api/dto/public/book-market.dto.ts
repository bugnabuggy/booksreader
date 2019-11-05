import { BookPrice } from '../../entities';
import { SubscriptionStatus } from '@br/core/enums/subscription-status';

export interface BookMarketDto {
    bookId: string;
    title: string;
    author: string;
    description: string;
    picture: string;
    published: Date;

    bookPrices: BookPrice[];
    subscription: SubscriptionStatus;
}
