import { BookMarketDto } from "@br/core/models/api/dto/public";
import { SubscriptionStatus } from '@br/core/enums/subscription-status';

const bookMarketDtoMock = {
    author: 'Test',
    title: 'Test book',
    description: 'Test description',
    isPublished: true,
    isForSale: true,
    picture: '',
    semanticUrl: '',
    bookId: 'test_book_id',
    authorId: 'test_author_id',
    published: new Date(),
    bookPrices: [
        {
            bookId: 'test_book_id',
            currencyId: 603,
            id: 'test_id',
            price: 900,
            created: new Date(),
            ownerId: ''
        }
    ],
    subscription: SubscriptionStatus.active
} as BookMarketDto;

export { bookMarketDtoMock }