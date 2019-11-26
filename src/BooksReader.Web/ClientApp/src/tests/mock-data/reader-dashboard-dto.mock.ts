import { ReaderDashboardDto } from '@br/core/models';
import { SubscriptionStatus } from '@br/core/enums/subscription-status';

const readerDashboardDtoMock = {
    book: {
        author: 'Test',
        bookId: 'test_book',
        picture: '',
        isForSale: true,
        title: 'Test book'
    },
    subscription: {
        bookId:'test_book',
        startDate: new Date(),
        subscriptionId: 'sub_id',
        status: SubscriptionStatus.active
    }
} as ReaderDashboardDto;

export { readerDashboardDtoMock };