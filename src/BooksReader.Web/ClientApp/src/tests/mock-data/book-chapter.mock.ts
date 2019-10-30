import { BookChapter } from '@br/core/models';

export const bookChapterMock  = {
    id: 'chapter_id',
    bookId: 'test_book_id',
    title: 'test chapter',
    isPublished: false,
    version: 0,
    number: 2,
    content: 'Hellow World!'
} as BookChapter;
