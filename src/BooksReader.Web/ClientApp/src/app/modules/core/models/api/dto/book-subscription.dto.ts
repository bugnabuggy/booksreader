import { SubscriptionStatus } from '@br/core/enums/subscription-status';

export interface BookSubscriptionDto {
    subscriptionId: string;
    bookId: string;
    startDate: Date;
    endDate?: Date;
    status: SubscriptionStatus
}
