import { environment } from '../../environments/environment';

class Endpoints {
    static baseApiUrl = environment.baseApiUrl;

    static forntend = {
    };
    static api = {
        authorization: {
            login: Endpoints.baseApiUrl + 'connect/token',
            logout: Endpoints.baseApiUrl + 'connect/revocation',
            registration: Endpoints.baseApiUrl + 'api/identity/registration',
            antiforgery: Endpoints.baseApiUrl + 'api/identity/antiforgery'
        },
        user: {
            info: Endpoints.baseApiUrl + 'api/identity/me',
            loginHistory: Endpoints.baseApiUrl + 'api/identity/log-history',
        },
        reader: {
            books: Endpoints.baseApiUrl + 'api/books',
        },
        users: {
            all: Endpoints.baseApiUrl + 'api/admin/users',
            userRole: Endpoints.baseApiUrl + 'api/admin/users/{username}/{role}',
        },
        author: {
            books: Endpoints.baseApiUrl + 'api/author/books',
            deleteBook: Endpoints.baseApiUrl + 'api/author/books/{id}'
        },
        admin: {
        }
    };
}

export { Endpoints };
