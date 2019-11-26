    import { environment } from '@br/env/environment';

class Endpoints {
    static baseApiUrl = environment.baseApiUrl;

    static areas =  {
        user: 'user',
        reader: 'reader',
        author: 'author',
        admin: 'admin'
    };

    static frontend = {
        main: 'main',
        authorization: 'authorize',
        authorizationUrl: '/authorize',

        registration: 'registration',
        registrationUrl: '/registration',

        error: 'error',

        public: {
            bookMarket: 'book-market',
            bookMarketUrl: '/book-market' ,

            book: 'book/:id',
            bookUrl: '/book/:id',

            authors: 'authors',
            authorsUrl: '/authors',
            authorUrl: '/authors/:id',
        },

        user: {
            profile: 'profile', 
            profileUrl: '/' + Endpoints.areas.user + '/profile',
            forceLogout: 'logout',
            forceLogoutUrl: '/logout',
            becomeAnAuthor: 'become-an-author',
            becomeAnAuthorUrl: '/' + Endpoints.areas.user + '/become-an-author'
        },
        reader: {
            profile: 'profile',
            profileUrl: '/' +  Endpoints.areas.reader + '/profile',
            dashboard: 'dashboard',
            dashboardUrl: '/' +  Endpoints.areas.reader + '/dashboard',
            readTheBook: 'read-book/:bookId',
            readTheBookUrl: '/' + Endpoints.areas.reader + '/read-book/:bookId',
        },
        admin: {
            dashboard: 'dashboard',
            dashboardUrl: '/' + Endpoints.areas.admin + '/dashboard',
            allUsers: 'users',
            allUsersUrl: '/' + Endpoints.areas.admin + '/users',
            allDomains: 'domains',
            allDomainsUrl: '/' + Endpoints.areas.admin + '/domains',
            allBooks: 'books',
            allBooksUrl: '/' + Endpoints.areas.admin + '/books',
            bookVerification: '/' + Endpoints.areas.admin + '/book/:id/verification'
        },
        author: {
            profile: 'profile',
            profileUrl: '/' + Endpoints.areas.author + '/profile',
            dashboard: 'dashboard',
            dashboardUrl: '/' + Endpoints.areas.author + '/dashboard',
            book: 'book/:id/:tab',
            bookUrl: '/' + Endpoints.areas.author + '/book/:id/:tab',
        }
        
    };
    
    static api = {
        authorization: {
            login: Endpoints.baseApiUrl + 'connect/token',
            logout: Endpoints.baseApiUrl + 'connect/revocation',
            registration: Endpoints.baseApiUrl + 'api/identity/registration',
            antiforgery: Endpoints.baseApiUrl + 'api/identity/antiforgery',
            changePassword: Endpoints.baseApiUrl + 'api/identity/change-password',
        },
        user: {
            info: Endpoints.baseApiUrl + 'api/identity/me',
            profile: Endpoints.baseApiUrl + 'api/user/profile',
            loginHistory: Endpoints.baseApiUrl + 'api/identity/login-history',
            becomeAnAuthor: Endpoints.baseApiUrl + 'api/user/author'
        },
        reader: {
            books: Endpoints.baseApiUrl + 'api/reader/books/{id}',
        },
        users: {
            all: Endpoints.baseApiUrl + 'api/admin/users',
            userRole: Endpoints.baseApiUrl + 'api/admin/users/{username}/{role}',
            delete: Endpoints.baseApiUrl + 'api/admin/users/{username}',
        },
        author: {
            profile: Endpoints.baseApiUrl + 'api/author/profile',
            fullProfile: Endpoints.baseApiUrl + 'api/author/profile/full',
            book: Endpoints.baseApiUrl + 'api/author/book/{id}',
            bookFullEditInfo: Endpoints.baseApiUrl + 'api/author/book/{id}/edit',
            chapter: Endpoints.baseApiUrl + 'api/author/book/{bookId}/chapter/{id}',
            reorderChapters:  Endpoints.baseApiUrl + 'api/author/book/{bookId}/chapter/reorder',
            price: Endpoints.baseApiUrl + 'api/author/book-price/{id}',
        },
        admin: {
            books: {
                all: Endpoints.baseApiUrl + 'api/admin/books',
                verification: Endpoints.baseApiUrl + 'api/admin/books/verification'
            }
        },
        public: {
            pageInfo: Endpoints.baseApiUrl + 'api/public',
            lists:  Endpoints.baseApiUrl + 'api/public/lists',
        },
        domains: {
            domain: Endpoints.baseApiUrl + 'api/domains',
            toggleState: Endpoints.baseApiUrl + 'api/domains/{id}/toggle',
        },
        publicPages: { 
            pages: Endpoints.baseApiUrl + 'api/public-pages'
        },
        booksMarket: {
            books: Endpoints.baseApiUrl + 'api/book-market',
        } 

    };
}

export { Endpoints };
