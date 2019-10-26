export enum PublicPageType {
        authorPage = 1,
        bookPage = 2,
        promoPage = 3
}

export const PublicPageTypeStrings = {
        [PublicPageType.authorPage] : 'PUBLIC_PAGES.AUTHOR_PAGE',
        [PublicPageType.bookPage] : 'PUBLIC_PAGES.BOOK_PAGE',
        [PublicPageType.promoPage] : 'PUBLIC_PAGES.PROMO_PAGE',
}
